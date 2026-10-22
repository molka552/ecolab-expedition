"""Exhaustively check the actual boolean expressions in GardenRules.cs."""
from pathlib import Path
from itertools import product
import re
import json

root = Path(__file__).resolve().parents[1]
source = (root/'Assets/Scripts/GardenRules.cs').read_text()

def expression(method):
    match = re.search(r'public static bool '+method+r'\([^)]*\)\s*\{\s*return (.*?);\s*\}', source)
    assert match, method
    value = match.group(1).replace('&&',' and ').replace('||',' or ')
    value = re.sub(r'!(?!=)', ' not ', value)
    return value.replace('input.Length','len(input)').strip()

checks = [
    ('Light', ('sun','water','smoke'), list(product([False, True], repeat=3)), (True,True,False)),
    ('Water', ('left','middle','right'), list(product(range(4), repeat=3)), (1,2,3)),
    ('Cycle', ('input',), [(list(x),) for x in product(range(3), repeat=3)], ([0,1,2],))
]
for name, keys, cases, solution in checks:
    expr = expression(name)
    passed = [case for case in cases if eval(expr, {'__builtins__':{}, 'len':len}, dict(zip(keys,case)))]
    assert passed == [solution], (name,passed)
    print(name+': PASS, '+str(len(cases))+' cases, unique solution '+str(solution))
    if name == 'Cycle':
        for seq in [[],[0],[0,1],[0,1,2,0]]:
            assert not eval(expr, {'__builtins__':{}, 'len':len}, {'input':seq})
json.loads((root/'Packages/manifest.json').read_text())
assert '2022.3.62f2' in (root/'ProjectSettings/ProjectVersion.txt').read_text()
for f in ['Assets/Scenes/Main.unity','Assets/Scripts/GardenGame.cs','Assets/Scripts/GardenUI.cs','Assets/Editor/EcoLabTools.cs','Assets/Resources/RuntimeMaterial.mat']:
    assert (root/f).is_file(),f
print('Project structure, packages and incomplete sequence checks: PASS')
