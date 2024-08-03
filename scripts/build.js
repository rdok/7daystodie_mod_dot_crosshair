const {readFileSync, unlinkSync, rmSync, cpSync, writeFileSync} = require("fs");
const {join} = require("path");
const {execSync} = require("child_process");

const modInfoXmlPath = join(__dirname, "..", "DotCrosshair/ModInfo.xml");
const modInfoXmlRaw = readFileSync(modInfoXmlPath, "utf8");

const versionMatch = modInfoXmlRaw.match(/<Version value="([\d.]+)"/);
if (!versionMatch) throw new Error("Version not found in ModInfo.xml");
const version = versionMatch[1];

const modNameMatch = modInfoXmlRaw.match(/<Name value="([\S]+)"/);
if (!modNameMatch) throw new Error("Name not found in ModInfo.xml");
const modName = modNameMatch[1];

const artifact = `${modName} ${version}.7z`;
const srcDir = join(__dirname, "../DotCrosshair/bin/Release");
const distDir = join(__dirname, "..", "dist");
const buildDir = join(distDir, modName);

try {
  unlinkSync(artifact);
  rmSync(distDir, {recursive: true});
} catch (e) {
  // Expected behaviour if it doesn't exist.
}

cpSync(srcDir, buildDir, {recursive: true});

execSync(
    `.\\node_modules\\7z-bin\\win32\\7z.exe a "${artifact}" "${buildDir}"`,
);
