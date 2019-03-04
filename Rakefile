require 'rake'
require 'rake/clean'

# --------------------------------------------------------------------------- #
# Configuration
# --------------------------------------------------------------------------- #
SOLUTION    = 'Cube.Xui'
BRANCHES    = [ 'stable', 'net35' ]
TESTTOOLS   = [ 'NUnit.ConsoleRunner', 'OpenCover', 'ReportGenerator' ]
TESTCASES   = { 'Cube.Xui.Tests' => 'Tests' }

# --------------------------------------------------------------------------- #
# Commands
# --------------------------------------------------------------------------- #
COPY        = 'cp -pf'
CHECKOUT    = 'git checkout'
BUILD       = 'msbuild /t:Clean,Build /m /verbosity:minimal /p:Configuration=Release;Platform="Any CPU";GeneratePackageOnBuild=false'
PACK        = 'nuget pack -Properties "Configuration=Release;Platform=AnyCPU"'
TEST        = '../packages/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe'

# --------------------------------------------------------------------------- #
# Tasks
# --------------------------------------------------------------------------- #
task :default do
    Rake::Task[:clean].execute
    Rake::Task[:build].execute
    Rake::Task[:pack].execute
end

# --------------------------------------------------------------------------- #
# Restore
# --------------------------------------------------------------------------- #
task :restore do
    sh("nuget restore #{SOLUTION}.sln")
    TESTTOOLS.each { |src| sh("nuget install #{src}") }
end

# --------------------------------------------------------------------------- #
# Build
# --------------------------------------------------------------------------- #
task :build do
    BRANCHES.each do |branch|
        sh("#{CHECKOUT} #{branch}")
        Rake::Task[:restore].execute
        sh("#{BUILD} #{SOLUTION}.sln")
    end
end

# --------------------------------------------------------------------------- #
# Pack
# --------------------------------------------------------------------------- #
task :pack do
    sh("#{CHECKOUT} net35")
    sh("#{PACK} Libraries/#{SOLUTION}.nuspec")
    sh("#{CHECKOUT} master")
end

# --------------------------------------------------------------------------- #
# Test
# --------------------------------------------------------------------------- #
task :test do
    Rake::Task[:restore].execute
    sh("#{BUILD} #{SOLUTION}.sln")

    fw  = `git symbolic-ref --short HEAD`.chomp
    fw  = 'net45' if (fw != 'net35')
    bin = 'bin/Any CPU/Release'
    TESTCASES.each { |proj, dir| sh("#{TEST} \"#{dir}/#{bin}/#{fw}/#{proj}.dll\"") }
end

# --------------------------------------------------------------------------- #
# Clean
# --------------------------------------------------------------------------- #
CLEAN.include("#{SOLUTION}.*.nupkg")
CLEAN.include(%w{bin obj}.map{ |e| "**/#{e}/*" })