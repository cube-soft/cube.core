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
PROJECT     = "Cube.Forms"
MAIN        = "#{PROJECT}"
LIB         = "../packages"
CONFIG      = "Release"
BRANCHES    = ["master", "net35"]
PLATFORMS   = ["Any CPU"]
PACKAGES    = ["Libraries/#{PROJECT}.nuspec"]

# --------------------------------------------------------------------------- #
# commands
# --------------------------------------------------------------------------- #
BUILD = "msbuild -v:m -t:build -p:Configuration=#{CONFIG}"
PACK  = %(nuget pack -Properties "Configuration=#{CONFIG};Platform=AnyCPU")

# --------------------------------------------------------------------------- #
# clean
# --------------------------------------------------------------------------- #
CLEAN.include("#{PROJECT}.*.nupkg")
CLEAN.include("#{LIB}/cube.*")
CLEAN.include(["bin", "obj"].map{ |e| "**/#{e}" })

# --------------------------------------------------------------------------- #
# default
# --------------------------------------------------------------------------- #
desc "Clean, build, and create NuGet packages."
task :default => [:clean, :build_all, :pack]

# --------------------------------------------------------------------------- #
# pack
# --------------------------------------------------------------------------- #
desc "Create NuGet packages in the net35 branch."
task :pack do
    checkout("net35") { PACKAGES.each { |e| sh("#{PACK} #{e}") }}
end

# --------------------------------------------------------------------------- #
# build
# --------------------------------------------------------------------------- #
desc "Build projects in the current branch."
task :build, [:platform] do |_, e|
    e.with_defaults(:platform => PLATFORMS[0])
    sh("nuget restore #{PROJECT}.sln")
    sh(%(#{BUILD} -p:Platform="#{e.platform}" #{PROJECT}.sln))
end

# --------------------------------------------------------------------------- #
# build_all
# --------------------------------------------------------------------------- #
desc "Build projects in pre-defined branches and platforms."
task :build_all do
    BRANCHES.product(PLATFORMS).each { |set|
        checkout(set[0]) do
            Rake::Task[:build].reenable
            Rake::Task[:build].invoke(set[1])
        end
    }
end

# --------------------------------------------------------------------------- #
# checkout
# --------------------------------------------------------------------------- #
def checkout(branch, &callback)
    sh("git checkout #{branch}")
    callback.call()
ensure
    sh("git checkout master")
end
