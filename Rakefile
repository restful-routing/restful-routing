require "rubygems"
require "bundler"
Bundler.setup

desc "remove files in output directory"
task :clean do
  puts ">>> Removing output <<<"
  Dir["#{site}/*"].each { |f| rm_rf(f) }
end

desc "Generate site files only"
task :generate_site => [:clean] do
  puts "\n\n>>> Generating site files <<<"
  system "bundle exec staticmatic build ."
end

desc "generate and deploy website to github user pages"
multitask :deploy_github do
  puts ">>> Deploying #{deploy_branch} branch to Github Pages <<<"
  require 'git'
  repo = Git.open('.')
  puts "\n>>> Checking out #{deploy_branch} branch <<<\n"
  repo.branch("#{deploy_branch}").checkout
  (Dir["*"] - [site]).each { |f| rm_rf(f) }
  Dir["#{site}/*"].each {|f| mv(f, ".")}
  rm_rf(site)
  puts "\n>>> Moving generated site files <<<\n"
  Dir["**/*"].each {|f| repo.add(f) }
  repo.status.deleted.each {|f, s| repo.remove(f)}
  puts "\n>>> Commiting: Site updated at #{Time.now.utc} <<<\n"
  message = ENV["MESSAGE"] || "Site updated at #{Time.now.utc}"
  repo.commit(message)
  puts "\n>>> Pushing generated site to #{deploy_branch} branch <<<\n"
  repo.push
  puts "\n>>> Github Pages deploy complete <<<\n"
  repo.branch("#{source_branch}").checkout
end

task :preview do
  require "staticmatic"
  configuration = StaticMatic::Configuration.new
  
  directory = "/users/steve/projects/restful-routing"

  config_file = "#{directory}/src/configuration.rb"

  if File.exists?(config_file)
    config = File.read(config_file)
    eval(config)
  end
  staticmatic = StaticMatic::Base.new(directory, configuration)
  staticmatic.run("preview")
end