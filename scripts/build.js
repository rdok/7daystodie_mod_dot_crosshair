const { readFileSync, writeFileSync, unlinkSync, rmSync, cpSync, copyFileSync } = require("fs");
const { join } = require("path");
const { execSync } = require("child_process");

const args = process.argv.slice(2);
if (args.length < 4) {
  console.error("Usage: node script.js <absoluteOutputPath> <modName> <modDisplayName> <modDescription> <modVersion>");
  process.exit(1);
}

const absoluteOutputPath = args[0];
const newModName = args[1];
const newDisplayName = args[2];
const newDescription = args[3];
const newVersion = args[4];

const modInfoXmlPath = join(absoluteOutputPath, "ModInfo.xml");
const modInfoXmlBackupPath = join(__dirname, "..", "ModInfo.xml.bck");

try {
  copyFileSync(modInfoXmlPath, modInfoXmlBackupPath);
  console.log("Backup created: ModInfo.xml.bck");
} catch (error) {
  console.error("Error creating backup:", error);
  process.exit(1);
}

let modInfoXmlRaw = readFileSync(modInfoXmlPath, "utf8");

const updatedModInfoXmlRaw = modInfoXmlRaw
    .replace(/{{PROJECT_NAME}}/, newModName)
    .replace(/{{PROJECT_DISPLAY_NAME}}/, newDisplayName)
    .replace(/{{PROJECT_DESCRIPTION}}/, newDescription)
    .replace(/{{VERSION_NAME}}/, newVersion);
console.debug('updatedModInfoXmlRaw', updatedModInfoXmlRaw);


try {
  writeFileSync(modInfoXmlPath, updatedModInfoXmlRaw);
  console.log(`Updated ModInfo.xml with name: ${newModName}, display name: ${newDisplayName}, description: ${newDescription}, version: ${newVersion}`);
} catch (error) {
  console.error("Error updating ModInfo.xml:", error);

  copyFileSync(modInfoXmlBackupPath, modInfoXmlPath);
  console.log("Restored original ModInfo.xml from backup.");
  process.exit(1);
}

const artifact = `${newModName}_${newVersion}.7z`;
const distDir = join(__dirname, "..", "dist");
const buildDir = join(distDir, newModName);

try {
  unlinkSync(artifact);
  rmSync(distDir, { recursive: true });
} catch (e) {
  // Expected behavior if it doesn't exist.
}

cpSync(absoluteOutputPath, buildDir, { recursive: true });

execSync(
    `.\\node_modules\\7z-bin\\win32\\7z.exe a "${artifact}" "${buildDir}"`
);

console.log(`Build completed: ${artifact}`);

copyFileSync(modInfoXmlBackupPath, modInfoXmlPath);
console.log("Restored original ModInfo.xml from backup.");
unlinkSync(modInfoXmlBackupPath);
