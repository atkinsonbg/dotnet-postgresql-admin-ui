build:
	dotnet build ./Admin/Admin.csproj -r Debug 

certs:
	dotnet dev-certs https --clean
	dotnet dev-certs https --verbose

up:
	docker-compose up -d

down:
	docker-compose down