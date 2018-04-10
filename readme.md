# Styx

## Stop entering your credentials by setting up SSH on your computer

1. First or all, make sure you can see hidden folders and files on your computer, by clicking on the View menu in File Explorer and checking Hidden Files. [How to show your hidden files](https://support.microsoft.com/en-us/help/4028316/windows-view-hidden-files-and-folders-in-windows-10)
2. Secondly, you need to generate a SSH key on your computer. Open a command prompt by typing CMD in your Windows Explorer and clicking on Command Prompt
3. Next paste in your command prompt this line (right click then paste): **ssh-keygen -t rsa -b 4096**, the wizard will then ask with some question, simply hit Enter for every question until the wizard generate your SSH key.
4. Now, you need to setup git kraken. Go to File -> Preferences.. -> Authentication. Then in the SSH Public key section, click on Browse and select your id_rsa.pub file C:/users/{YOUR_USER_NAME}/.ssh/id_rsa.pub and click the Copy to clipboard button. 
5. Finally, add your brand new SSH key to your gitlab account. Go to your profile settings and click on the SSH Keys tab. Copy the content of your clipboard (CTRL + V) into the **Key** textarea in gitlab, and add a **Title** to it then click on the **Add key** button.

## CLone the project with GitKraken 

[Download GitKraken](https://www.gitkraken.com/)

To clone the Styx project on your computer, you need to go to File -> Clone Repo, then select the Clone with URL tab and browse the folder where you want to download the project and also paste the SSH Url of the Styx project that you can found on gitlab **git@gitlab.com:ragecure/styx.git**

## Styx guidelines

### Naming conventions

- Folders and files name must be in english.
- Source code is written in english.
- Assets naming must be in english.

### Hiearchy

- The hiearchy in the Styx project must be respected
    Assets
        _Scenes (.unity)
        _Scripts (.cs files)
            Common (RageCure utilities)
        _Arts 
            2D (.png files)
            3D (.fbx and .obj files)
            Materials (.mat files)
            Textures (.tga files)
            Shaders (.shader files)
        _Prefabs (.prefab files)
        Resources (if we need to instantiate a prefab during runtime, we need to put the .prefab file here)
    Builds (.exe files)

- The hiearchy in a Unity Scene must be clean all the time. We will add a hierarchy template for Unity scene soon.
        


