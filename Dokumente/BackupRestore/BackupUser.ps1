# Abfrage nach Datenbankname
$allDatabases = mongosh -u "admin" -p "admin" --authenticationDatabase "admin" --quiet --eval "show dbs"
$databaseList = $allDatabases -split "`n" | Select-Object -Skip 2
Write-Host "Vorhandene Datenbanken auf localhost:"
foreach ($database in $databaseList) {
    Write-Host $database
}
$database = Read-Host "Welche Datenbank möchten Sie sichern?"

# Öffne Explorer, um Pfad und Namen für Backup-Datei auszuwählen
$saveFile = [System.Windows.Forms.SaveFileDialog]::new()
$saveFile.Filter = "Bson-Files (*.bson)|*.bson|All Files (*.*)|*.*"
$saveFile.ShowDialog() | Out-Null
$backupPath = $saveFile.FileName

# Erstelle Backup mit mongodump-Befehl
mongodump  -u "admin" -p "admin" --authenticationDatabase "admin" --db $database --out $backupPath
Write-Host "Das Backup der Datenbank $database wurde erfolgreich im Pfad $backupPath gespeichert."
