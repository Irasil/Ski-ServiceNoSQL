$fbd = New-Object System.Windows.Forms.FolderBrowserDialog
$fbd.Description = "Wählen Sie den Restore-Ordner"
$fbd.RootFolder = "Desktop"
$fbd.ShowNewFolderButton = $false
if ($fbd.ShowDialog() -eq "OK")
{
  $folderPath = $fbd.SelectedPath
  Write-Host "Der ausgewählte Ordnerpfad ist: $folderPath"
}
$database = Read-Host "Wie heisst die Datenbank die Sie wiederherstellen möchten?"

if ($folderPath) {
  mongorestore -u "admin" -p "admin" --authenticationDatabase "admin" --db $database $folderPath
}
