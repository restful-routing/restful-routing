using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.RouteDebug
{
    public class RouteDebugController : Controller
    {
        public class RouteDebugViewModel
        {
            public string DebugPath { get; set; }
            public IList<RouteInfo> RouteInfos { get; set; }
        }

        public class RouteInfo
        {
            public int Position { get; set; }
            public string HttpMethod { get; set; }
            public string Path { get; set; }
            public string Endpoint { get; set; }
            public string Area { get; set; }
            public string Namespaces { get; set; }
            public string Name { get; set; }
        }

        public ActionResult Index()
        {
            var model = new RouteDebugViewModel { RouteInfos = new List<RouteInfo>() };
            int position = 1;
            foreach (var route in RouteTable.Routes.Select(x => x as Route).Where(x => x != null))
            {
				// issue: #33 Fix
                var httpMethodConstraint = (route.Constraints ?? new RouteValueDictionary())["httpMethod"] as HttpMethodConstraint;

                ICollection<string> allowedMethods = new string[] { };
                if (httpMethodConstraint != null)
                {
                    allowedMethods = httpMethodConstraint.AllowedMethods;
                }

                var namespaces = new string[] { };
                if (route.DataTokens != null && route.DataTokens["namespaces"] != null)
                    namespaces = (route.DataTokens["namespaces"] ?? new string[0]) as string[];
                var defaults = new RouteValueDictionary();
                if (route.Defaults != null)
                    defaults = route.Defaults;
                if (route.DataTokens == null)
                    route.DataTokens = new RouteValueDictionary();

                var namedRoute = route as NamedRoute;
                var routeName = "";
                if (namedRoute != null)
                {
                    routeName = namedRoute.Name;
                }

                model.RouteInfos.Add(new RouteInfo
                {
                    Position = position,
                    HttpMethod = string.Join(", ", allowedMethods.ToArray()),
                    Path = route.Url,
                    Endpoint = defaults["controller"] + "#" + defaults["action"],
                    Area = route.DataTokens["area"] as string,
                    Namespaces = string.Join(" ", (namespaces).ToArray()),
                    Name = routeName
                });
                position++;
            }

            var debugPath = (from p in model.RouteInfos
                             where p.Endpoint.Equals("routedebug#resources", StringComparison.InvariantCultureIgnoreCase)
                             select p.Path.Replace("{name}", string.Empty)).FirstOrDefault();
            model.DebugPath = debugPath;
     
            return Content(Debugger(model));
        }

        public string Debugger(RouteDebugViewModel model) 
        {
            var routeInfos = new StringBuilder();
            foreach (var routeinfo in model.RouteInfos) 
            {
                routeInfos.AppendLine("<tr>");
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Position);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Endpoint);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.HttpMethod);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Name);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Area);
                routeInfos.AppendFormat("<td class='path'><a href='{0}' target='_blank'>{0}</a></td>", routeinfo.Path);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Namespaces);
                routeInfos.AppendLine("</tr>");
            }

            return HtmlPage.Replace("{{routes}}", routeInfos.ToString());
        }


const string HtmlPage =
    @"<!DOCTYPE html>
    <html lang=""en"">
      <head>
        <meta charset=""utf-8"">
        <title>Restful Routing - Route Debugger</title>
        <!-- Le styles -->
        <link href=""//netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css"" rel=""stylesheet"">
        <link rel='stylesheet' type='text/css' href='//ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.4/css/jquery.dataTables.css'>
        <script type='text/javascript' charset='utf8' src='//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js'></script>
        <script src='//netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/js/bootstrap.min.js'></script>
        <script type='text/javascript' charset='utf8' src='//ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.4/jquery.dataTables.min.js'></script>

        <style>
            body {
                padding-top: 60px;
            }

            div.dataTables_length label {
	            float: left;
	            text-align: left;
            }

            div.dataTables_length select {
	            width: 75px;
            }

            div.dataTables_filter label {
	            float: right;
            }

            div.dataTables_info {
	            padding-top: 8px;
            }

            div.dataTables_paginate {
	            float: right;
	            margin: 0;
            }

            table.table {
	            clear: both;
	            margin-bottom: 6px !important;
	            max-width: none !important;
            }

            table.table thead .sorting,
            table.table thead .sorting_asc,
            table.table thead .sorting_desc,
            table.table thead .sorting_asc_disabled,
            table.table thead .sorting_desc_disabled {
	            cursor: pointer;
	            *cursor: hand;
            }

            table.table thead .sorting { background: none no-repeat center right; }
            table.table thead .sorting_asc { background: none no-repeat center right; }
            table.table thead .sorting_desc { background: none no-repeat center right; }

            table.table thead .sorting_asc_disabled { background: none no-repeat center right; }
            table.table thead .sorting_desc_disabled { background: none no-repeat center right; }

            table.dataTable th:active {
	            outline: none;
            }

            /* Scrolling */
            div.dataTables_scrollHead table {
	            margin-bottom: 0 !important;
	            border-bottom-left-radius: 0;
	            border-bottom-right-radius: 0;
            }

            div.dataTables_scrollHead table thead tr:last-child th:first-child,
            div.dataTables_scrollHead table thead tr:last-child td:first-child {
	            border-bottom-left-radius: 0 !important;
	            border-bottom-right-radius: 0 !important;
            }

            div.dataTables_scrollBody table {
	            border-top: none;
	            margin-bottom: 0 !important;
            }

            div.dataTables_scrollBody tbody tr:first-child th,
            div.dataTables_scrollBody tbody tr:first-child td {
	            border-top: none;
            }

            div.dataTables_scrollFoot table {
	            border-top: none;
            }




            /*
                * TableTools styles
                */
            .table tbody tr.active td,
            .table tbody tr.active th {
	            background-color: #08C;
	            color: white;
            }

            .table tbody tr.active:hover td,
            .table tbody tr.active:hover th {
	            background-color: #0075b0 !important;
            }

            .table-striped tbody tr.active:nth-child(odd) td,
            .table-striped tbody tr.active:nth-child(odd) th {
	            background-color: #017ebc;
            }

            table.DTTT_selectable tbody tr {
	            cursor: pointer;
	            *cursor: hand;
            }

            div.DTTT .btn {
	            color: #333 !important;
	            font-size: 12px;
            }

            div.DTTT .btn:hover {
	            text-decoration: none !important;
            }


            ul.DTTT_dropdown.dropdown-menu a {
	            color: #333 !important; /* needed only when demo_page.css is included */
            }

            ul.DTTT_dropdown.dropdown-menu li:hover a {
	            background-color: #0088cc;
	            color: white !important;
            }

            /* TableTools information display */
            div.DTTT_print_info.modal {
	            height: 150px;
	            margin-top: -75px;
	            text-align: center;
            }

            div.DTTT_print_info h6 {
	            font-weight: normal;
	            font-size: 28px;
	            line-height: 28px;
	            margin: 1em;
            }

            div.DTTT_print_info p {
	            font-size: 14px;
	            line-height: 20px;
            }



            /*
                * FixedColumns styles
                */
            div.DTFC_LeftHeadWrapper table,
            div.DTFC_LeftFootWrapper table,
            table.DTFC_Cloned tr.even {
	            background-color: white;
            }

            div.DTFC_LeftHeadWrapper table {
	            margin-bottom: 0 !important;
	            border-top-right-radius: 0 !important;
	            border-bottom-left-radius: 0 !important;
	            border-bottom-right-radius: 0 !important;
            }

            div.DTFC_LeftHeadWrapper table thead tr:last-child th:first-child,
            div.DTFC_LeftHeadWrapper table thead tr:last-child td:first-child {
	            border-bottom-left-radius: 0 !important;
	            border-bottom-right-radius: 0 !important;
            }

            div.DTFC_LeftBodyWrapper table {
	            border-top: none;
	            margin-bottom: 0 !important;
            }

            div.DTFC_LeftBodyWrapper tbody tr:first-child th,
            div.DTFC_LeftBodyWrapper tbody tr:first-child td {
	            border-top: none;
            }

            div.DTFC_LeftFootWrapper table {
	            border-top: none;
            }

            table.dataTable td { 
                word-wrap: break-word; 
            }
        </style>

        <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
          <script src=""//html5shim.googlecode.com/svn/trunk/html5.js""></script>
        <![endif]-->

        <script type=""text/javascript"">
        /* Set the defaults for DataTables initialisation */
        $.extend( true, $.fn.dataTable.defaults, {
	        ""sDom"": ""<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>"",
	        ""sPaginationType"": ""bootstrap"",
	        ""oLanguage"": {
		        ""sLengthMenu"": ""_MENU_ records per page""
	        }
        } );


        /* Default class modification */
        $.extend( $.fn.dataTableExt.oStdClasses, {
	        ""sWrapper"": ""dataTables_wrapper form-inline""
        } );


        /* API method to get paging information */
        $.fn.dataTableExt.oApi.fnPagingInfo = function ( oSettings )
        {
	        return {
		        ""iStart"":         oSettings._iDisplayStart,
		        ""iEnd"":           oSettings.fnDisplayEnd(),
		        ""iLength"":        oSettings._iDisplayLength,
		        ""iTotal"":         oSettings.fnRecordsTotal(),
		        ""iFilteredTotal"": oSettings.fnRecordsDisplay(),
		        ""iPage"":          Math.ceil( oSettings._iDisplayStart / oSettings._iDisplayLength ),
		        ""iTotalPages"":    Math.ceil( oSettings.fnRecordsDisplay() / oSettings._iDisplayLength )
	        };
        };


        /* Bootstrap style pagination control */
        $.extend( $.fn.dataTableExt.oPagination, {
	        ""bootstrap"": {
		        ""fnInit"": function( oSettings, nPaging, fnDraw ) {
			        var oLang = oSettings.oLanguage.oPaginate;
			        var fnClickHandler = function ( e ) {
				        e.preventDefault();
				        if ( oSettings.oApi._fnPageChange(oSettings, e.data.action) ) {
					        fnDraw( oSettings );
				        }
			        };

			        $(nPaging).addClass('pagination').append(
				        '<ul>'+
					        '<li class=""prev disabled""><a href=""#"">&larr; '+oLang.sPrevious+'</a></li>'+
					        '<li class=""next disabled""><a href=""#"">'+oLang.sNext+' &rarr; </a></li>'+
				        '</ul>'
			        );
			        var els = $('a', nPaging);
			        $(els[0]).bind( 'click.DT', { action: ""previous"" }, fnClickHandler );
			        $(els[1]).bind( 'click.DT', { action: ""next"" }, fnClickHandler );
		        },

		        ""fnUpdate"": function ( oSettings, fnDraw ) {
			        var iListLength = 5;
			        var oPaging = oSettings.oInstance.fnPagingInfo();
			        var an = oSettings.aanFeatures.p;
			        var i, j, sClass, iStart, iEnd, iHalf=Math.floor(iListLength/2);

			        if ( oPaging.iTotalPages < iListLength) {
				        iStart = 1;
				        iEnd = oPaging.iTotalPages;
			        }
			        else if ( oPaging.iPage <= iHalf ) {
				        iStart = 1;
				        iEnd = iListLength;
			        } else if ( oPaging.iPage >= (oPaging.iTotalPages-iHalf) ) {
				        iStart = oPaging.iTotalPages - iListLength + 1;
				        iEnd = oPaging.iTotalPages;
			        } else {
				        iStart = oPaging.iPage - iHalf + 1;
				        iEnd = iStart + iListLength - 1;
			        }

			        for ( i=0, iLen=an.length ; i<iLen ; i++ ) {
				        // Remove the middle elements
				        $('li:gt(0)', an[i]).filter(':not(:last)').remove();

				        // Add the new list items and their event handlers
				        for ( j=iStart ; j<=iEnd ; j++ ) {
					        sClass = (j==oPaging.iPage+1) ? 'class=""active""' : '';
					        $('<li '+sClass+'><a href=""#"">'+j+'</a></li>')
						        .insertBefore( $('li:last', an[i])[0] )
						        .bind('click', function (e) {
							        e.preventDefault();
							        oSettings._iDisplayStart = (parseInt($('a', this).text(),10)-1) * oPaging.iLength;
							        fnDraw( oSettings );
						        } );
				        }

				        // Add / remove disabled classes from the static elements
				        if ( oPaging.iPage === 0 ) {
					        $('li:first', an[i]).addClass('disabled');
				        } else {
					        $('li:first', an[i]).removeClass('disabled');
				        }

				        if ( oPaging.iPage === oPaging.iTotalPages-1 || oPaging.iTotalPages === 0 ) {
					        $('li:last', an[i]).addClass('disabled');
				        } else {
					        $('li:last', an[i]).removeClass('disabled');
				        }
			        }
		        }
	        }
        } );


        /*
         * TableTools Bootstrap compatibility
         * Required TableTools 2.1+
         */
        if ( $.fn.DataTable.TableTools ) {
	        // Set the classes that TableTools uses to something suitable for Bootstrap
	        $.extend( true, $.fn.DataTable.TableTools.classes, {
		        ""container"": ""DTTT btn-group"",
		        ""buttons"": {
			        ""normal"": ""btn"",
			        ""disabled"": ""disabled""
		        },
		        ""collection"": {
			        ""container"": ""DTTT_dropdown dropdown-menu"",
			        ""buttons"": {
				        ""normal"": """",
				        ""disabled"": ""disabled""
			        }
		        },
		        ""print"": {
			        ""info"": ""DTTT_print_info modal""
		        },
		        ""select"": {
			        ""row"": ""active""
		        }
	        } );

	        // Have the collection use a bootstrap compatible dropdown
	        $.extend( true, $.fn.DataTable.TableTools.DEFAULTS.oTags, {
		        ""collection"": {
			        ""container"": ""ul"",
			        ""button"": ""li"",
			        ""liner"": ""a""
		        }
	        } );
        }


        /* Table initialisation */
        $(document).ready(function() {
	        $('#routes').dataTable( {
		        ""sDom"": ""<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>"",
		        ""sPaginationType"": ""bootstrap"",
                ""iDisplayLength"" : 100,
		        ""oLanguage"": {
			        ""sLengthMenu"": ""_MENU_ records per page""
		        }
	        } );
        } );
        </script>
        
      </head>

      <body>

        <div class=""navbar navbar-inverse navbar-fixed-top"">
          <div class=""navbar-inner"">
            <div class=""container-fluid"">
              <a class=""btn btn-navbar"" data-toggle=""collapse"" data-target="".nav-collapse"">
                <span class=""icon-bar""></span>
                <span class=""icon-bar""></span>
                <span class=""icon-bar""></span>
              </a>
              <a class=""brand"" href=""#"">Route Debugger</a>
              <div class=""nav-collapse collapse"">
                <ul class=""nav"">
                  <li><a href=""/""><i class=""icon-white icon-home""></i> Root</a></li>
                  <li><a href=""http://restfulrouting.com""><i class=""icon-white icon-question-sign""></i> Documentation</a></li>
                  <li><a href=""https://github.com/stevehodgkiss/restful-routing""><i class=""icon-white icon-heart""></i> Github Repository</a></li>
                </ul>
              </div><!--/.nav-collapse -->
            </div>
          </div>
        </div>

        <div class=""container-fluid"">
        <div class=""row-fluid"">
            <table cellpadding='0' cellspacing='0' border='0' class='table table-striped table-bordered' id='routes'>
              <thead>
                <tr>
                    <th>#</th>
                    <th>Endpoint</th>
                    <th>HttpMethods</th>
                    <th>Name</th>
                    <th>Area</th>
                    <th>Path</th>                
                    <th>Namespace</th>
                </tr>
              </thead>
              <tbody>
                {{routes}}
              </tbody>
            </table>
        </div>

        </div> <!-- /container -->
      </body>
    </html>";

    }
}