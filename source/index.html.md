---
title: Restful Routing

language_tabs:

toc_footers:
  - <a href='https://github.com/restful-routing/restful-routing'>Fork On GitHub</a>

includes:

search: true
---

# Introduction

**Restful Routing for ASP.NET MVC, based on the Ruby on Rails routing DSL. It utlizes HTTP verbs to keep unique paths to a minimum, while optimizing your controllers structure.**

> PM > Install-Package RestfulRouting

Welcome to Restful Routing for ASP.NET MVC! Routing is arguably at the core of all ASP.Net MVC applications. Managing routes yourself can be cumbersome, frustrating, and unproductive. Some developers utilize a catch-all route, which may lead to unexpected problems as the application grows. **Restful Routing solves the routing management issue by providing an opinionated interface of registering routes**.

We hope you find that this library is so easy to use that you will wonder how you ever developed ASP.Net MVC applications without it.

## Golden Seven

Restful Routing is based around the idea of `Resources` and the Golden Seven actions:

Action | HTTP Method
------ | ------
INDEX  | GET
SHOW   | GET
CREATE | POST
NEW    | GET
EDIT   | GET
UPDATE | PUT
DESTROY| DELETE

The aim of Restful Routing is to make you concious of your controllers. While you may bend the rules when appropriate, you should really focus on keeping all controllers limited to these seven actions. If you fight the conventions of Restful Routing, you will not be leveraging the strength of our opinionated framework and be miserable doing so.

## Default Route

```csharp
// BAD! DO NOT USE!
routes.MapRoute(
    "Default",                                              // Route name
    "{controller}/{action}/{id}",                           // URL with parameters
    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
);
```
<aside class="warning">
DO NOT USE THE DEFAULT ROUTE!
</aside>

Before diving into Restful Routing, I would like to discuss the default route provided by ASP.NET MVC. Found in many starter templates, it is a helpful tool when getting started, but I do not recommend you use it for any production environment. I will list the reasons below:

- Any controller in your project will be accessible even if not registered with a routing framework like Restful Routing.
- It can lead to unexpected outward route resolution and hours of debugging.

# Getting Started

## Step 1. Installing Restful Routing

To get started, we will assume you are starting with an existing ASP.Net MVC project and understand how to get to that point. 

Open up the Package Manager Console and type the following:
`PM> Install-Package RestfulRouting`.

If you do not wish to use the Package Manager Console, you can right-click your web project and select the "Manage Nuget Packages" context menu item. Search for `RestfulRouting` install the package.

## Step 2. Configuring Restful Routing

```csharp 
// Routes.cs
using System.Web.Routing;
using RestfulRouting.Documentation.Controllers;
[assembly: WebActivator.PreApplicationStartMethod(typeof(RestfulRouting.Documentation.Routes), "Start")]
namespace RestfulRouting.Documentation
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");
            /*
             * TODO: Add your routes here.
             * 
            map.Root<HomeController>(x => x.Index());
            
            map.Resources<BlogsController>(blogs =>
            {
                blogs.As("weblogs");
                blogs.Only("index", "show");
                blogs.Collection(x => x.Get("latest"));
                blogs.Resources<PostsController>(posts =>
                {
                    posts.Except("create", "update", "destroy");
                    posts.Resources<CommentsController>(c => c.Except("destroy"));
                });
            });
            map.Area<Controllers.Admin.BlogsController>("admin", admin =>
            {
                admin.Resources<Controllers.Admin.BlogsController>();
                admin.Resources<Controllers.Admin.PostsController>();
            });
             */
        }
        public static void Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoutes<Routes>();
        }
    }
}
```

A couple of changes should have happened to your project, namely two files were added: `Routes.cs` and `ApplicationController`.

`Routes.cs` should look like the section to the right. We have a little help already included for you in this file. Not everyone is writing a blog application, so feel free to erase the commented code after you have looked over it.

**Note the dependency on WebActivator, this will ensure the routes are registred when your application starts.** You can choose to remove this dependency if you would rather call the `Start` method yourself.

```csharp
// setting the root route
map.Root<HomeController>(x => x.Index());
```

For your configuration, be sure to set the `Root`;

In addition to `Routes.cs` you will also get `ApplicationController`. Using this base controller is *optional*, but it does have the code necessary to utilize **Format Results**

## Step 3. View Engine Configuration (Optional)

```csharp
// Global.asax
using System.Web.Mvc;
namespace RestfulRouting.Documentation 
{
    public class MvcApplication : System.Web.HttpApplication 
    {
        protected void Application_Start() 
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
        }
    }
}
```

ASP.Net MVC depends on view engines to locate your views. While the default view engine is ok, we found that the folder structure to be a bit verbose. If you would like to utilize our view engines, then change your Global.asax file to look like the following. 

There You Have It. Code! Code! Code!

If you want to learn more about how and what is happening in the registration of your routes continue reading.

# RouteSet

In this section, you will learn about `RouteSet`. The class is critical to making Restful Routing work.

## Basics

```csharp
public class Routes : RouteSet
{
    public override void Map(IMapper map)
    {
        map.DebugRoute("routedebug");
        map.Root<HomeController>(x => x.Index());
    }
}
```

**A RouteSet is a collection of route mappings**. It not only defines routes in your application, but it can connect to other RouteSets, creating a rich set of routes with minimal effort.

All routes are defined inside the `Map` method of a RouteSet.

The IMapper instance passed into Map presents a way to map your routes to the `RouteTable` dictionary at the heart of every ASP.Net MVC application. If you installed Restful Routing through Nuget, you will have noticed a RouteSet already created for you.

<aside class="notice">
Note: This code just needs to be called at application start. We prefer WebActivator, but you can call it in Global.asax if you would like. MapRoutes is an extension method from RestfulRouting.
</aside> 

## Connecting

```csharp
public class Routes : RouteSet
{
    public override void Map(IMapper map)
    {
        map.DebugRoute("routedebug");
        map.Connect<One>();
        map.Connect<Two>();
        map.Connect<APIRoutes>("api", new string[] { "RestfulRouting.Controllers.API" });
    }
}

public class One : RouteSet
{
    public override void Map(IMapper map)
    {
        map.Resource<OneController>();
    }
}

public class Two : RouteSet
{
    public override void Map(IMapper map)
    {
        map.Resources<TwosController>();
    }
}

public class APIRoutes : RouteSet
{
    public override void Map(IMapper map)
    {
        map.Resource<RestfulRouting.Controllers.API.OneController>();
        map.Resources<RestfulRouting.Controllers.API.TwosController>();
    }
}
```

Sometimes you want to logically seperate your RouteSets. Instead of grouping all routes in a single RouteSet, we provide the ability to connect RouteSets. You must select one RoutSet to be the root of your RouteSets, after which you may connect other RouteSets any way you would like. RouteSets also support namespaces, similar to areas.


## Configuration (Optional)

A RouteSet provides settings that can modify the behavior of route registration. These properties can be found within the context of a RouteSet. *Most developers will not want to touch these features*.

Setting | Default | Result
---------- | ------- | -------
LowercaseUrls | `true` | register urls in lowercase invariant culture
LowercaseDefaults | `true` | register your actions when registered normally in lowercase invariant culture
LowercaseActions | `true` | register your actions when registered with Route in lowercase invariant culture
MapDelete | `false` | register an additional action of **Delete [GET]**. Used to display a delete confirmation page before removing a resource.

# Mapping

Restful Routing is a mapping framework, and to do the mapping we provide a set of concepts and interfaces. Read about each interface below and what functionality each provides in detail.

## Root

```csharp
map.Root<HomeController>(x => x.Index());
```

Every application needs a default page, and registering a root path has never been easier. If your application was hosted at `http://example.com` then that would be considered the root.

Your application may only have **one** root.

## Resource

```csharp
map.Resource<ProfileController>();
```

A resource is a *"thing"* that can be thought of singularly from the perspective of the application user. A profile for an authenticated user is a good example of a resource. Take a look at the following url. When it comes to a profile, you will derive the identifier via a cookie (or other implicit means), and do not need the identifier in the path of the route.

<aside class="note">
http://localhost/profile
</aside>

The url assumes that the application knows what a profile is and how to present it. Now how would the registration look in a RouteSet?

### Actions and Http Methods

When registering a resource, a set of actions and methods are registered automatically. Register your controller and implement the actions.

<aside class="info">
Note: All routes are registred with an HttpVerb method constraint, so you will need to use HttpMethodOverride when posting from a form.
</aside>

Action | HTTP Method
------ | ------
SHOW   | GET
CREATE | POST
NEW    | GET
EDIT   | GET
UPDATE | PUT
DESTROY| DELETE

For our example with the `ProfileController`, you would get the following output:

Action | HTTP Method | Result (controller#action) | Url
------ | ----------- | -------------------------- | ---
SHOW   | GET         | profile#show               | /profile
NEW    | GET         | profile#new                | /profile/new
CREATE | POST        | profile#create             | /profile
EDIT   | GET         | profile#edit               | /profile/edit
UPDATE | PUT         | profile#update             | /profile
DESTROY| DELETE      | profile#destroy            | /profile


### Naming

```csharp
// NAMING
map.Resource<ProfileController>(profile => profile.As("my-secret-identity"));

// NAMING
/// in RouteSet
resource.PathNames(p => {
    p.CreateName = "creating";
    p.DeleteName = "deleting";
    p.DestroyName = "destroying";
    p.EditName = "editing";
    p.NewName = "newing";
});

// NAMING
/// The resource controller    
public class ResourceController: ApplicationController {
    public ActionResult Creating() {
        //...
    }
}
```

Sometimes you name your controller one thing, but wish to have the resource url reflect something entirely else. This is simple to do.

The new url will for `profile#show` will be changed to `http://localhost/my-secret-identity`.

Additionally, you can change the default action names of a resource by using the `PathNames` method. **This isn't recommended, but we like to give you options**.

### Choosing Actions

```csharp
// CHOOSING ACTIONS
/// register only actions we need
map.Resource<ProfileController>(profile => {
     profile.Only("show", "edit", "update")
});
/// exclude actions we don't need
map.Resource<ProfileController>(profile =>{
     profile.Except("new", "create", "destroy")
});
```

Sometimes you might not need all the methods we provide for a resource. Imagine the following examples:

- As a user, I should not be able to create a profile, because it is created implicitly when we registered.
- As a user, I should be able to edit my current profile.
- As a user, I should not be able to delete my profile.
- As a user, I should be able to see my profile
 
After review, we only need to register the following actions: `Show`, `Edit`, `Update`. To limit our resource to those actions, we would use the following code in our RouteSet.

### Nesting a Resource

```csharp
// NESTING A RESOURCE
map.Resource<ProfileController>(profile => {
    profile.Resource<CreditCardController>()
});
```

A Resource sometimes depend on another resource. When nesting a resource, you have a one to one relationship between the parent resource to its dependent resource.
 
If you create a `creditcard` resource, and nest it within our `profile` resource (as noted on the right) you would get the resulting routes, excluding the profile routes:

Action | HTTP Method | Result (controller#action)    | Url Template
------ | ----------- | ----------------------------- | -------------------
SHOW   | GET         | creditcard#show               | /profile/creditcard
NEW    | GET         | creditcard#new                | /profile/creditcard/new
CREATE | POST        | creditcard#create             | /profile/creditcard
EDIT   | GET         | creditcard#edit               | /profile/creditcard/edit
UPDATE | PUT         | creditcard#update             | /profile/creditcard
DESTROY| DELETE      | creditcard#destroy            | /profile/creditcard

<aside class="info">
Note: You may also nest a resource within resources and vice versa.
</aside>

## Resources

```csharp
map.Resources<ProductsController>();
```

Resources represent collections withing your domain. Imagine building a store application, you might want to expose your catalog so that customers may search and purchase products. Products would be considered resources. The url that is generated would also reflect that there are many products to be offered.

<aside>http://localhost/products</aside>

Notice the **pluralization** of products. If you are starting an e-commerce site, you would most likely sell more than one product at a time.

### Actions

When registering resources, a set of actions and methods are registered automatically. Register your controller and implement the actions.

<aside class="info">    
Note: All routes are registred with an HttpVerb method constraint, so you will need to use HttpMethodOverride when posting from a form.
</aside>

Action | HTTP Method
------ | ------
INDEX  | GET
SHOW   | GET
CREATE | POST
NEW    | GET
EDIT   | GET
UPDATE | PUT
DESTROY| DELETE

For our example with the `ProductsController`, you would get the following output:

Action | HTTP Method | Result (controller#action)  | Url Template
------ | ----------- | --------------------------- | ------------
INDEX  | GET         | products#index              | /products
SHOW   | GET         | products#show               | /products/{id}
NEW    | GET         | products#new                | /products/new
CREATE | POST        | products#create             | /products
EDIT   | GET         | products#edit               | /products/{id}/edit
UPDATE | PUT         | products#update             | /products/{id}
DESTROY| DELETE      | products#destroy            | /products/{id}

<aside class="info">
<strong>id</strong> is a <strong>required</strong> parameter. The route will not resolve unless specified.
</aside>

### Naming

```csharp
// NAMING
/// in RouteSet
map.Resources<ProductsController>(products => {
    products.As("amazing-items")
});

// NAMING
/// in RouteSet
resources.PathNames(p => {
    p.CreateName = "creating";
    p.DeleteName = "deleting";
    p.DestroyName = "destroying";
    p.EditName = "editing";
    p.NewName = "newing";
});

/// controller
public class ResourcesController: ApplicationController {
    public ActionResult Creating() {
       //...
    }
    //...
}
```

Sometimes you name your controller one thing, but wish to have the resources url reflect something entirely else. This is simple to do.

The new url will for products#index will be changed.

<aside>
http://localhost/amazing-items
</aside>

Additionally, you can change the default **action** names of a resource by using the `PathNames` method. This isn't recommended, but we like to give you options.

### Customizing The `{id}` Parameter

```csharp
// CUSTOMIZING THE ID PARAMETER
map.Resources<UsersController>(users => users.IdParameter("username"));

// CUSTOMIZING THE ID PARAMETER
public class UsersController: Controller {
    // not id, but username
    public ActionResult Show(string username) {
       //...
    }
}
```

By default, the name `id` is used for the resource id parameter in route URL patterns. For example, the URL pattern for the products#show action in Actions and HTTP Methods above is `/products/{id}`.

In some scenarios, you might want to use a different name for the parameter. Maybe you want the parameter name to match the name of the property used to identify the resource:
    
The route added for the users#show action would then have the URL pattern `/users/{username}`.

### Choosing Actions

```csharp
// CHOOSING ACTIONS
/// register only actions we need
map.Resource<ProductsController>(products => {
     products.Only("show", "edit", "update")
});
/// exclude actions we don't need
map.Resource<ProductsController>(profile =>{
     products.Except("new", "create", "destroy")
});
```

Sometimes you might not need all the methods we provide. You may remove actions using the follwing methods on the right.


### Nesting Resources

```csharp
map.Resources<ProductsController>(products => {
    products.Resources<ReviewsController>()
});
```

Resources sometimes depend on other resources. When nesting resources, you have a one to many relationship between the parent resource to its dependent resources. Think about a product that has reviews.To accomplish this you would register the two resources like so.

Action | HTTP Method | Result (controller#action)  | Url Template
------ | ----------- | --------------------------- | ---
INDEX  | GET         | reviews#index               | /products/{productId}/reviews
SHOW   | GET         | reviews#show                | /products/{productId}/reviews/{id}
NEW    | GET         | reviews#new                 | /products/{productId}/reviews/new
CREATE | POST        | reviews#create              | /products/{productId}/reviews
EDIT   | GET         | reviews#edit                | /products/{productId}/reviews/{id}/edit
UPDATE | PUT         | reviews#update              | /products/{productId}/reviews/{id}
DESTROY| DELETE      | reviews#destroy             | /products/{productId}/reviews/{id}   

<aside class="info">
<strong>id</strong> and <strong>productId</strong> are <strong>required</strong> parameter. The route will not resolve unless <strong>both</strong> are specified.
</aside>

## Member And Collection Routes

```csharp
// MEMBER AND COLLECTION ROUTES
/// IN ROUTESET
area.Resources<ResourcesController>(resources => {
    resources.Collection(r => r.Get("many"));
    resources.Member(r => r.Get("lonely"));
});
area.Resource<ExtrasController>(extras => {
    // using member, notice collection is unavailable
    extras.Member(e => e.Get("member"));
});
```

```text
<!-- IN RAZOR -->
<a class="btn" href="@Url.Action("member", "extras")">Member on a Resource</a>
<a class="btn" href="@Url.Action("lonely", "resources", new { id = 1 })">Member on Resources</a>
<a class="btn" href="@Url.Action("many", "resources")">Collection on Resources</a>
```

Use the `Member` and `Collection` methods inside of resources to add routes to the collection (without an id route parameter) or the member (with an id route parameter). Methods available to map are Get, Post, Put, Delete and Head. When within the context of a resource (singular), you will only have access to the `Member` method.

## Standard Mapping

```csharp
// STANDARD MAPPING
// IN ROUTE SET
area.Resource<ExtrasController>(extras => {
    // Use path
    extras.Path("using_path")
        .To<ExtrasController>(e => {
             e.UsingPath()
        }).GetOnly();
    // Using route
    extras.Route(
        new Route("mappings/extras/with_route",
            new RouteValueDictionary(new {
                controller = "extras", 
                action = "usingroute",
                area="mappings" 
            }),
            new MvcRouteHandler()
        ) 
    );
});
```

```text
<!-- IN RAZOR VIEW -->
<a class="btn" href="@Url.Action("usingpath", "extras")">Path</a>
<a class="btn" href="@Url.Action("usingroute", "extras")">Route</a>
```

Sometimes our conventions just don't work for you. We may be opinionated, but we do not rule with an iron fist.

Two additional methods are provided so you may add custom routes: Path is a mix of traditional ASP.Net MVC routing syntax with our interface wrapper. Route is a very traditional way of adding a custom route.

## Areas

```csharp
// IN ROUTESET
map.Area<AreasController>("mappings", area => {
    area.Resource<ResourceController>();
    area.Resources<ResourcesController>(resources => {
        // we are nesting a resource inside of another resource
        resources.Resources<OtherResourcesController>(other => {
             other.Only("index")
        });
    });
    area.Resource<ExtrasController>();
});
```

Areas are handled by namespace in Restful Routing. Usually there is a correlation between folders and namespaces, so for most developers this isn't a big difference from what they already do.

The controller provided to the Area method is used to determine the namespace for the grouped controllers. The "mappings" string is used to determine the name of the area, along with the url part.

```text
<!-- IN RAZOR VIEW -->
<!-- GENERATE URL IN AN AREA -->
@Url.Action("index", "resources", new { area = "mappings" })
<!-- GENERATE URL OUTSIDE OF AREA -->
@Url.Action("show", "quickstart", new { area = "" })
```
Remember you will need to provide the area when generating urls. To exit out of an area, you can set the area to an empty string. All rules with a Resource and Resources still apply when dealing with Areas.

# Extras
## View Engines

```csharp
    public class RestfulRoutingRazorViewEngine : RazorViewEngine
    {
        public RestfulRoutingRazorViewEngine()
        {
            AreaMasterLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/{1}/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.cshtml",
            };

            AreaViewLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/{1}/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.vbhtml",
            };

            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }
    }
```

Restful Routing comes with two view engines: `RestfulRoutingRazorViewEngine` and `RestfulRoutingWebFormViewEngine`. Let's look at the one of these view engines.

These view engines change the way that ASP.Net MVC will look for views.

<aside class="info">
Using our view engines is completely optional, but can help reduce the folders necessary to create areas in your code.
</aside>

```text
# DEFAULT VIEWENGINE BEHAVIOR

Views
    |-  Home            // home controller
    |-  Profiles        // profiles controller
Areas
    |- Administration  // administration area
        |-Controllers  // area controllers
        |-Models       // models for areas
        |-Views
            |-Users        // users views in adminstration area
            |-Products     // products views in administration area
            |-Shared       // area shared views
etc...
```

```text
# RESTFUL ROUTING VIEWENGINE BEHAVIOR

Controllers
    |- Administration   // controllers for administration
Models
    |-Administration    // models for administration
Views
    |-  Home            // home controller
    |-  Profiles        // profiles controller
    |-  Administration  // administration area
        |- Users        // users controller in adminstration area
        |- Products     // products controller in administration area
etc...
```

## Format Result

This is a feature that allows you to use implicit format extensions on a resource and resources. There are two core methods to this feature: RespondTo, WithFormatRoutes.

<aside class="warning">
If you are building a full blown API, we recommend you use <a href="http://www.asp.net/web-api">WebAPI</a> instead of ASP.NET MVC as it has more powerful capabilities.
</aside>

```csharp
namespace RestfulRouting.Documentation.Controllers
{
    public abstract class ApplicationController : Controller {
        protected ActionResult RespondTo(Action<FormatCollection> format) {
            return new FormatResult(format);
        }
    }
}
```

```csharp
// IN ROUTESET
// enable format routes
extras.WithFormatRoutes();
```

We provide a set of known types out of the box to make using our fluent interface a little nicer, but we support any file extension you can think of. To generate these format routes just specify the format as follows.

```csharp
// IN CONTROLLER
public ActionResult Awesome() {
    var model = new Awesome {
        date = DateTime.UtcNow.ToShortDateString(),
        id = "awesomwe_id",
        message = "hey you are seeing this in message"
    };
    
    // contrived example
    return RespondTo(format => {
        format.Default = RedirectToAction("show");
        format.Json = () => {
            model.id = "awesome_json";
            return Json(model, JsonRequestBehavior.AllowGet);
        };
        format.Xml = () => {
            model.id = "awesome_xml";
            return Xml(model);
        };
        format["yml"] = () => {
            model.id = "awesome_yml";
            return View("awesome.yml", model);
        };
        format.Html = () => {
            Flash.Success("Nothing to see here");
            return RedirectToAction("show");
        };
    });
}
```

```text
<!-- IN RAZOR VIEW -->
@@Url.Action("usingpath", "extras", new { format = "json" })   // generate json url
@@Url.Action("usingroute", "extras", new { format = "xml" }})  // generate xml url
```

Awesome right?! We recommend you read the following section of **Quirks / Gotchas** if you plan on using format routes.
his is helpful if you are building single page applications Use sparingly and understand when it is appropriate to seperate functionality in your actions.

## Redirect Dead Links

```csharp
public class RedirectRouteSet : RouteSet
{
    public RedirectRouteSet() {
        // you can register the 
        // attribute somewhere else, 
        // it is here
        // for demo purposes
        GlobalFilters.Filters.Add(new RedirectRouteFilter());
    }
    public override void Map(IMapper map)
    {
        // Redirect an old path, to a new one
        map.Redirect("this/is/old")
            .To(new { controller = "home", action = "index" })
            .GetOnly();
        map.Redirect("into/area")
            .To<ExtrasController>(e => e.Show(), "mappings")
            .GetOnly();
    }
}  
```

There are times when we make better url decisions than others. We want the ability to change the urls, without breaking existing urls. This is only necessary once your application goes live. This mostly applies to public facing sites, but could be useful in an intranet situtation.

The first thing to note, is you should register all your redirect routes as the last routes in your list. This is because they are the least important. The will only ever be hit when accessing an old link. Your application should not be generating these urls, and if it is then you have issues.

Secondly, the registration looks like RestfulRouting's good old syntax, but it doesn't require you to nest it within any definition. I would recommed, creating a RouteSet called DeadLinks or Redirects and connecting it at the end of your master RouteSet.

Lastly, and this is important, <strong>register the RedirectRouteFilterAttribte as a global filter.</strong>
    
As you can see to the right, you just set the old url, and provide values to resolve to the new url. We use the UrlHelper under the covers to find the best matching route.

# Route Debugger

```csharp
// IN ROUTESET
map.DebugRoute("routedebug");
```

Restful Routing comes with a powerful route debugger aimed at helping you understand your route mappings. You can enable and disable the route debugger purely by adding the following line to your RouteSet.

<aside class="warning">
Make sure to unregister the route debugger when deploying to your production environment. There is no performance hit, but you might not want to expose sensitive routes. </strong></p>
</aside>

# Quirks / Gotchas

This section attempts to detail some quirks with ASP.NET MVC and Restful Routing. These can cause issues sometimes and it is good to understand why and when they can impact your application.

## Persistant Route Values

```text
// MAKE REQUEST TO THIS URL

/mappings/resources/1

// ROUTE values
{ 
    id: 1,
    area: mappings,
    controller: resources 
}  

<!-- IN RAZOR VIEW -->
@Url.Action("index")

<!-- IS EQUIVALENT TO -->    
@Url.Action("index", new {
     controller = "resources",
     area = "mappings",
     id = 1 })
<!-- 
    BECAUSE MVC carries 
    over current route values
-->
```

ASP.Net MVC always tries to be helpful. Helpful, although well intentioned, can lead to some strange behavior sometimes. The biggest problem can be saved route values from your existing request.

Understanding this quirk can help you debug lots of issues, not just with Restful Routing, but with ASP.NET MVC in general. You can see the example to the right.

## Mixing Mapping Levels

```chsarp
map.Resource<ProfileController>(profile => {
    // DOH! should be profile
    // not map!
    // EXCEPTION!!!!
    map.Resource<CreditCardController>()
});
```

Restful Routing is designed to be fluent and nested. During the creation of your `RouteSet` you may accidently call the wrong parameter, leading to an error that may be difficult to diagnose. Our exception is helpful, but this is a common enough mistake that we decided to document it.

## Namespaces and Name Conflicts

Sometimes we want to name controllers the same, but have them in different namespaces. This is appropriate when building an administrative and front facing section of your site. Just remember to set the namespaces appropriately so that Routing can instantiate the correct controller.

