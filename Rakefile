# --------------------------------------------------------------------------- #
#
# Copyright (c) 2010 CubeSoft, Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#  http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
# --------------------------------------------------------------------------- #
require 'rake'
require 'rake/clean'

# --------------------------------------------------------------------------- #
# configuration
# --------------------------------------------------------------------------- #
PROJECT     = "Cube.Core"
LATEST      = "v9"
BRANCHES    = ["net47", "net60", "net35"]
PLATFORMS   = ["Any CPU"]
PACKAGES    = ["Libraries/Core/Cube.Core",
               "Libraries/Forms/Core/Cube.Forms",
               "Libraries/Forms/Controls/Cube.Forms.Controls",
               "Libraries/Xui/Cube.Xui",
               "Libraries/AlphaFS/Cube.FileSystem.AlphaFS",
               "Libraries/NLog/Cube.Logging.NLog",
               "Libraries/Trick/Cube.Trick",
               "Libraries/Tests/Cube.Private.Tests"]

# --------------------------------------------------------------------------- #
# clean
# --------------------------------------------------------------------------- #
CLEAN.include(["*.nupkg", "**/bin", "**/obj"])
CLOBBER.include("../packages/cube.*")

# --------------------------------------------------------------------------- #
# default
# --------------------------------------------------------------------------- #
desc "Clean, build, and create NuGet packages."
task :default => [:clean, :build_all] do
    checkout("#{LATEST}/net35") { Rake::Task[:pack].execute }
end

# --------------------------------------------------------------------------- #
# restore
# --------------------------------------------------------------------------- #
desc "Resote NuGet packages in the current branch."
task :restore do
    cmd("nuget restore #{PROJECT}.sln")
end

# --------------------------------------------------------------------------- #
# pack
# --------------------------------------------------------------------------- #
desc "Create NuGet packages."
task :pack do
    PACKAGES.each do |e|
        spec = File.exists?("#{e}.nuspec")
        pack = spec ?
               %(nuget pack -Properties "Configuration=Release;Platform=AnyCPU") :
               "dotnet pack -c Release --no-restore --no-build -o ."
        ext  = spec ? "nuspec" : "csproj"
        cmd("#{pack} #{e}.#{ext}")
    end
end

# --------------------------------------------------------------------------- #
# build
# --------------------------------------------------------------------------- #
desc "Build projects in the current branch."
task :build, [:platform] do |_, e|
    e.with_defaults(:platform => PLATFORMS[0])

    branch = %x(git rev-parse --abbrev-ref HEAD).chomp
    build  = branch.include?("net60") ?
             "dotnet build -c Release" :
             "msbuild -v:m -p:Configuration=Release"

    Rake::Task[:restore].execute
    cmd(%(#{build} -p:Platform="#{e.platform}" #{PROJECT}.sln))
end

# --------------------------------------------------------------------------- #
# build_all
# --------------------------------------------------------------------------- #
desc "Build projects in pre-defined branches and platforms."
task :build_all, [:version] do |_, e|
    e.with_defaults(:version => LATEST)

    BRANCHES.product(PLATFORMS).each do |bp|
        checkout("#{e.version}/#{bp[0]}") do
            Rake::Task[:build].reenable
            Rake::Task[:build].invoke(bp[1])
        end
    end
end

# --------------------------------------------------------------------------- #
# test
# --------------------------------------------------------------------------- #
desc "Test projects in the current branch."
task :test => [:build] do
    cmd("dotnet test -c Release --no-restore --no-build #{PROJECT}.sln")
end

# --------------------------------------------------------------------------- #
# checkout
# --------------------------------------------------------------------------- #
def checkout(branch, &callback)
    cmd("git checkout #{branch}")
    callback.call()
ensure
    cmd("git checkout master")
end

# --------------------------------------------------------------------------- #
# cmd
# --------------------------------------------------------------------------- #
def cmd(args)
    sh("cmd.exe /c #{args}")
end