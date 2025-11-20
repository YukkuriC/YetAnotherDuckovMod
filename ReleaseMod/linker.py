import os, subprocess

os.chdir(os.path.dirname(__file__))
mod_folder = r'C:\SteamLibrary\steamapps\common\Escape from Duckov\Duckov_Data\Mods'
targets = list(filter(lambda f: os.path.isdir(f), os.listdir('.')))

print('Targets:')
for i, x in enumerate(targets):
    print(f'[{i}]: {x}')
target = targets[int(input())]

subprocess.run(
    [
        'cmd',
        '/c',
        'mklink',
        '/J',
        os.path.abspath(os.path.join(mod_folder, target)),
        os.path.abspath(target),
    ],
    shell=True,
)
