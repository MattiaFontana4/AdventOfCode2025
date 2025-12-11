
import numpy as np
from scipy.spatial.distance import pdist, squareform
with open("C:\\AdventOfCode\\Day8\\input.txt") as f:
    positions = f.read().splitlines()

# build graph 
# find pairs of 3d positions that are closest by l2 dist
points = []
for line in positions:
    x, y, z = map(int, line.split(","))
    points.append((x, y, z))

points = np.array(points)
dist_matrix = squareform(pdist(points))

# Create list of all edges with distances
edges = []
n = len(points)
for i in range(n):
    for j in range(i + 1, n):
        edges.append((i, j, dist_matrix[i, j]))

# Sort by distance
edges.sort(key=lambda x: x[2])

# start creating circuits from closest pair and iteratively add closest pairs
subgraphs = [set((edges[0][0], edges[0][1]))]
for edge in edges[1:1001]:
    u, v, dist = edge
    u_sg = None
    v_sg = None
    for sg in subgraphs:
        if u in sg:
            u_sg = sg
        if v in sg:
            v_sg = sg
    
    if u_sg is None and v_sg is None:
        subgraphs.append(set([u, v]))
    elif u_sg is None:
        v_sg.add(u)
    elif v_sg is None:
        u_sg.add(v)
    elif u_sg is not v_sg:
        # Both are in different subgraphs - merge them
        u_sg.update(v_sg)
        subgraphs.remove(v_sg)
        
three_largest_sg = sorted(subgraphs, key=lambda x: len(x), reverse=True)[:3]
print(f"Sizes of three largest subgraphs: {[len(sg) for sg in three_largest_sg]}")
print(f"Product of these three subgraphs: {len(three_largest_sg[0]) * len(three_largest_sg[1]) * len(three_largest_sg[2])}")

# part 2:
# keep connecting until all points are in one subgraph
# start new subgraphs 
subgraphs = [set((i,)) for i in range(len(points))]
for i in range(len(edges)):
    edge = edges[i]
    u, v, dist = edge
    u_sg = None
    v_sg = None
    for sg in subgraphs:
        if u in sg:
            u_sg = sg
        if v in sg:
            v_sg = sg
    
    # only need to connect since all subgraphs are initialized
    if u_sg is not v_sg:
        u_sg.update(v_sg)
        subgraphs.remove(v_sg)
    
    if len(subgraphs) == 1:
        last_edge_x = points[u][0]
        second_last_edge_x = points[v][0]
        print(f"Product of x coords: {last_edge_x * second_last_edge_x}")
        break

