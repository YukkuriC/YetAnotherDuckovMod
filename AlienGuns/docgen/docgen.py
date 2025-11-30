import os, json, re
from jinja2 import Environment, FileSystemLoader

os.chdir(os.path.dirname(__file__))
env = Environment(loader=FileSystemLoader('templates'))

if 'funcs':

    def escape_md(raw):
        raw = re.sub(
            r"\<color=(.+?)\>",
            lambda match: f'<span style="color={match.group(1)}">**',
            raw,
        )
        raw = raw.replace("</color>", "**</span>")
        return raw

    def prepare(lang):
        with open(f"../assets/lang/{lang}.json", encoding='utf-8') as f:
            raw = json.load(f)
        res = []
        i = 0
        while 1:
            keyTitle = f"YukkuriC.AlienGun.{i}"
            if not keyTitle in raw:
                break
            res.append(
                {
                    "idx": i,
                    "name": raw[keyTitle],
                    "descrip": raw.get(keyTitle + "_Desc", "").split("\n"),
                }
            )
            i += 1
        return res

    def gen(template, target):
        with open(target, 'w', encoding='utf-8') as f:
            print(env.get_template(template).render(**globals()), file=f)


gen("README_en.jinja", "../README_en.md")
gen("README_cn.jinja", "../README.md")
