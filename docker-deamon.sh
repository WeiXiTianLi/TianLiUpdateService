docker rm -f /tianli.update_service
docker run --name tianli.update_service -v ~/service/data/:/app/database -p 34072:80 -d tianli.update-api:dev