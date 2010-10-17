require 'rubygems'
require 'albacore'

PROJECT = 'RestfulRouting'

task :default => [:clean, :compile, :test]

desc 'removes build files'
task :clean do
	FileUtils.rm_rf("build")
end

desc 'compile'
msbuild :compile => :clean do |msb|
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