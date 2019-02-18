require 'rake'
require 'rake/clean'

# --------------------------------------------------------------------------- #
# Configuration
# --------------------------------------------------------------------------- #
SOLUTION    = 'Cube.FileSystem'
BRANCHES    = [ 'stable', 'net35' ]

# --------------------------------------------------------------------------- #
# Commands
# --------------------------------------------------------------------------- #
COPY        = 'cp -pf'
CHECKOUT    = 'git checkout'
BUILD       = 'msbuild /t:Clean,Build /m /verbosity:minimal /p:Configuration=Release;Platform="Any CPU";GeneratePackageOnBuild=false'
RESTORE     = 'nuget restore'
PACK        = 'nuget pack -Properties "Configuration=Release;Platform=AnyCPU"'

# --------------------------------------------------------------------------- #
# Tasks
# --------------------------------------------------------------------------- #
task :default do
    Rake::Task[:clean].execute
    Rake::Task[:build].execute
    Rake::Task[:pack].execute
end

# --------------------------------------------------------------------------- #
# Build
# --------------------------------------------------------------------------- #
task :build do
    BRANCHES.each do |branch|
        sh("#{CHECKOUT} #{branch}")
        sh("#{RESTORE} #{SOLUTION}.sln")
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
# Clean
# --------------------------------------------------------------------------- #
CLEAN.include("#{SOLUTION}.*.nupkg")
CLEAN.include(%w{dll log}.map{ |e| "**/*.#{e}" })