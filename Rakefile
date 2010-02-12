require 'rubygems'

PROJECT = 'RestfulRouting'

task :default => [:clean, :compile, :test]

desc 'removes build files'
task :clean do
	FileUtils.rm_rf("build")
end

desc 'compile'
task :compile => :clean do
	msbuild_path = File.join(ENV['windir'].dup, 'Microsoft.NET', 'Framework', 'v3.5', 'msbuild.exe')
	sh "#{msbuild_path} src\\#{PROJECT}.sln /maxcpucount /v:m /property:BuildInParallel=false /property:Configuration=release /property:Architecture=x86 /t:Rebuild"
	
	FileUtils.mkdir_p 'build'
	
	Dir.glob(File.join("src/#{PROJECT}/bin/Release", "*.{dll,pdb,xml,exe}")) do |file|
		copy(file, 'build')
	end
end

desc 'runs tests'
task :test do
	sh "tools\\mspec\\mspec.exe src\\#{PROJECT}.Tests\\bin\\Release\\#{PROJECT}.Tests.dll"
end