$date = Get-Date -Format "dd/MM/yyyy"
$backup = $Env:HOMEPATH + "\MongoBackup" + $date
mongodump -u "admin" -p "admin" --authenticationDatabase "admin" --db Ski --out $backup

