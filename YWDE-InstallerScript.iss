; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "You Will Do Evil"
#define MyAppVersion "1.4.2.1"
#define MyAppPublisher "Driven Time"
#define MyAppURL "https://zhakalendk.github.io/"
#define MyAppExeName "YWDE_Launcher.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7ED1DD4B-EAE8-4DB8-B06F-B6D5B3F7A3D7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\YWDE
DisableDirPage=yes
DefaultGroupName=YWDE
AllowNoIcons=yes
LicenseFile=E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\License.txt
InfoBeforeFile=E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\Agreement.txt
InfoAfterFile=E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\ReadMe.txt
OutputDir=E:\YWDERoot\Installer
OutputBaseFilename=YWDE
SetupIconFile=E:\YWDERoot\You-Will-Do-Evil\YWDE\YWDE_Launcher\YWDE_LauncherIcon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "danish"; MessagesFile: "compiler:Languages\Danish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\YWDE_Launcher.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "E:\YWDERoot\You-Will-Do-Evil\YWDE\Build\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

