import json
from pprint import pprint

with open("arena.json", encoding="utf-8") as f:
    data = json.load(f)

key = next(iter(data))

print("Top-level key:", key)
print("Type:", type(data[key]))
print("Length:", len(data[key]))
print()

pprint(data[key][0])