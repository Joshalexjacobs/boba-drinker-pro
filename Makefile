help:
	@fgrep -h "##" $(MAKEFILE_LIST) | sed -e 's/##//' | tail -n +2

build-all: ## Build All
	make build-webgl

build-webgl: ## Build WebGL
	"$(shell find-unity)/Contents/MacOS/Unity" -projectPath "$(shell pwd)/" -batchmode -quit -executeMethod "BobaDrinkerPro.Editor.Build.BuildWebGL"

serve-webgl: ## Serve WebGL build files
	./bin/server.mjs

clean: ## Clean untracked files from directory
	git clean -xdf
