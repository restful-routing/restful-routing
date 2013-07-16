require 'rubygems'
require 'albacore'

PROJECT = 'RestfulRouting'

task :default => [:clean, :compile, :spec]

desc 'removes build files'
task :clean do
	FileUtils.rm_rf("build")
end

desc 'generates assembly info'
assemblyinfo :assemblyinfo do |asm|
	asm.version = "1.4.3"
	asm.product_name = "RestfulRouting"
	asm.title = "RestfulRouting"
	asm.description = "RestfulRouting is a routing library for ASP.NET MVC based on the Rails 3 routing DSL."
	asm.output_file = "src/RestfulRouting/Properties/AssemblyInfo.cs"
end

desc 'compile'
msbuild :compile => [:clean, :assemblyinfo] do |msb|
	msb.solution = "src\\#{PROJECT}.sln"
	msb.verbosity = 'minimal'
	msb.properties = { 
		:configuration => :Release, 
		:BuildInParallel => :false, 
		:Architecture => 'x86' 
	}
	msb.targets :Rebuild
	
	FileUtils.mkdir_p 'build'
	
	Dir.glob(File.join("src/#{PROJECT}/bin/Release", "*.{dll,pdb,xml,exe}")) do |file|
		copy(file, 'build')
	end
end

desc 'runs specs'
mspec :spec do |mspec|
	mspec.command = 'tools\\mspec\\mspec-x86-clr4.exe'
	mspec.assemblies "src\\#{PROJECT}.Spec\\bin\\Release\\#{PROJECT}.Spec.dll"
	mspec.html_output = "build\\specs.html"
end

desc 'Create the nuget distribution package'
task :package => :compile do
	system "src\\.nuget\\nuget.exe pack RestfulRouting.nuspec -o build"
end

desc 'Release to nuget'
task :release => :package do
	version = IO.read("RestfulRouting.nuspec").match(%r{\<version\>(.*)\</version\>})[1]
	raise("This version has already been committed.") if `git tag`.split(/\n/).include?(version)
	
	nuspecs = Dir['build/*.nupkg']
	raise "Could not find nupkg file" unless nuspecs.size == 1
	system "nuget push #{nuspecs.first}"
	
	system "git tag \"v#{version}\""
	system "git push origin master"
	system "git push origin master --tags"
end