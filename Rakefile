require 'rake'
require 'rake/clean'

# --------------------------------------------------------------------------- #
# Configuration
# --------------------------------------------------------------------------- #
SOLUTION    = 'Cube.Forms'
BRANCHES    = [ 'stable', 'net35' ]

# --------------------------------------------------------------------------- #
# Commands
# --------------------------------------------------------------------------- #
COPY        = 'cp -pf'
CHECKOUT    = 'git checkout'
BUILD       = 'msbuild /t:Clean,Build /m /verbosity:minimal /p:Configuration=Release;Platform="Any CPU";GeneratePackageOnBuild=false'
RESTORE     = 'nuget restore'
INSTALL     = 'nuget install'
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
# Restore
# --------------------------------------------------------------------------- #
task :restore do
    sh("#{RESTORE} #{SOLUTION}.sln")
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
# Clean
# --------------------------------------------------------------------------- #
CLEAN.include("#{SOLUTION}.*.nupkg")
CLEAN.include(%w{bin obj}.map{ |e| "**/#{e}/*" })