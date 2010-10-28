require 'rubygems'
require 'albacore'

PROJECT = 'RestfulRouting'

task :default => [:clean, :compile, :test]

desc 'removes build files'
task :clean do
	FileUtils.rm_rf("build")
end

desc 'generates assembly info'
assemblyinfo :assemblyinfo do |asm|
	asm.version = "1.0.1"
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

desc 'runs tests'
mspec :test do |mspec|
	mspec.path_to_command = 'tools\\mspec\\mspec.exe'
	mspec.assemblies "src\\#{PROJECT}.Tests\\bin\\Release\\#{PROJECT}.Tests.dll"
	mspec.html_output = "src\\#{PROJECT}.Tests\\Reports\\specs.html"
end