"""Validate the actual expedition maps: safe routes to seeds, notebook and console."""
from pathlib import Path
from collections import deque
import re

root=Path(__file__).resolve().parents[1]
source=(root/'Assets/Scripts/ExpeditionMaps.cs').read_text()
maps=[re.findall(r'"([#.@*!C?]+)"',b) for b in re.findall(r'new\[\]\s*\{([^}]+)\}',source)]
assert len(maps)==3
report=['# Verification des expeditions','','Solutions de navigation sans toucher les ronces. U=haut, D=bas, L=gauche, R=droite. Appuyer ensuite sur E pour ouvrir la borne.','']
for n,rows in enumerate(maps,1):
    assert len({len(r) for r in rows})==1
    assert all(c=='#' for c in rows[0]+rows[-1])
    assert all(r[0]==r[-1]=='#' for r in rows)
    def positions(ch):return [(x,y) for y,r in enumerate(rows) for x,c in enumerate(r) if c==ch]
    assert len(positions('@'))==len(positions('C'))==len(positions('?'))==1
    assert len(positions('*'))==3
    start=positions('@')[0]; console=positions('C')[0]
    objectives=positions('*')+positions('?')
    bits={p:1<<i for i,p in enumerate(objectives)}
    safe={(x,y) for y,r in enumerate(rows) for x,c in enumerate(r) if c not in '#!'}
    state=(start,0); q=deque([state]); parent={state:None}; solution=None
    while q:
        p,mask=q.popleft()
        if mask==15 and abs(p[0]-console[0])+abs(p[1]-console[1])<=1:
            s=(p,mask); path=[]
            while parent[s] is not None:
                s,move=parent[s];path.append(move)
            solution=''.join(reversed(path));break
        for letter,(dx,dy) in [('U',(0,-1)),('D',(0,1)),('L',(-1,0)),('R',(1,0))]:
            nxt=(p[0]+dx,p[1]+dy)
            if nxt not in safe:continue
            s=(nxt,mask|bits.get(nxt,0))
            if s not in parent:parent[s]=((p,mask),letter);q.append(s)
    assert solution is not None, f'Unreachable objective in map {n}'
    report.append(f'- Jardin {n} : {len(solution)} pas, route `{solution}`. Trois graines et carnet accessibles sans perte de coeur.')
    print(f'Map {n}: PASS; safe complete route ({len(solution)} steps): {solution}')
report+=['','Les expressions des enigmes sont verifiees separement par verify_rules.py.','La compilation et les interactions graphiques doivent etre testees dans Unity ; aucun editeur Unity dans cet environnement.']
(root/'VALIDATION.md').write_text('\n'.join(report)+'\n')
