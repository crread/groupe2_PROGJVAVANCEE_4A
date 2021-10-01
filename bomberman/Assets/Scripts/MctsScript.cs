using System.Collections.Generic;
using UnityEngine;

internal class SimulationStruct
{
    public Vector3 position;
    public string tag;
    public collider collider;

    public SimulationStruct(Vector3 position, string tag, collider collider)
    {
        this.position = position;
        this.tag = tag;
        this.collider = collider;
    }
    public SimulationStruct(SimulationStruct simulationStruct)
    {
        position = simulationStruct.position;
        tag = simulationStruct.tag;
        collider = simulationStruct.collider;
    }
}
internal struct bombStruct
{
    public Vector3 position;
    public collider collider;
    public collider[] explosionColliders;
    public float timer;
}

internal class collider
{
    public Vector2 position;
    public float size;

    public collider(Vector2 position, float size)
    {
        this.position = new Vector2(position.x - size / 2, position.y - size / 2);
        this.size = size;
    }

    public collider(Vector3 position, float size)
    {
        this.size = size;
        this.position = new Vector2(position.x - size / 2, position.z - size / 2);
    }

}
internal class agent
{
    public Vector3 position;
    public collider collider;
    public List<SimulationStruct> playerPositionOnMap;
    public float bombCooldown;
    public bool alive;
}
internal class Node
{
    public Node parent;
    public Node[] children;
    public int played;
    public int score;
    public agent player;
    public agent agent;
    public char state;
    
    public List<SimulationStruct> freePlaces;
    public List<SimulationStruct> usedPlaces;
    public Node(Node parent)
    {
        player = new agent();
        agent = new agent();
        this.parent = parent;
        played = 0;
        score = 0;
    }
}

public class MctsScript : MonoBehaviour
{
    private float interval_between_frame;

    private Node _tree;
    
    [SerializeField]
    private float speedPlayer;
    
    [SerializeField]
    private float bombCooldown;
    
    [SerializeField]
    private GameObject agent;
    [SerializeField]
    private CharacterControllerScript agentController;
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CharacterControllerScript playerController;
    
    [SerializeField] private List<TrackerCollider> trackers = new List<TrackerCollider>();
    
    private List<SimulationStruct> freePlaces;
    private List<SimulationStruct> usedPlaces;
    private agent agentGlobal;
    private agent playerGlobal;
    private List<bombStruct> bombs;

    [SerializeField]
    private int minint = 10;

    public char GetNextMove()
    {
        interval_between_frame = 1 / Time.deltaTime;

        bombs = new List<bombStruct>();
        freePlaces = new List<SimulationStruct>();
        usedPlaces = new List<SimulationStruct>();
        agentGlobal = new agent();
        playerGlobal = new agent();

        InitRoot();
        InitMapArray();

        freePlaces.ForEach(item =>
        {
            _tree.freePlaces.Add(new SimulationStruct(item));
        });

        usedPlaces.ForEach(item =>
        {
            _tree.usedPlaces.Add(new SimulationStruct(item));
        });

        Node node = _tree;

        for (int depth = 0; depth < 1; depth++)
        {
            if (depth == 0)
            {
                ProcessSimulation(node);
            }
            else
            {
                node = Selection(node);
                InitNode(node);
                var actions = InitAction(node.agent);
                node.children = ConvertActionToList(node, actions);
                applyAction(node);
                ProcessSimulation(node);
            }
        }

        node = Selection(_tree);
        return node.state;
    }

    private void applyAction(Node node)
    {
        // node.
    }
    private void InitRoot()
    {
        var positionPlayer = player.transform.position;
        var positionAgent = agent.transform.position;
        
        _tree = new Node(null)
        {
            state = 'i',
            player =
            {
                position = positionAgent,
                alive = true,
                collider = new collider(positionAgent, 0.9f),
                bombCooldown = agentController.CurrentBombCooldown,
                playerPositionOnMap = new List<SimulationStruct>(),
            },
            agent =
            {
                position = positionPlayer,
                alive = true,
                collider = new collider(positionPlayer, 0.9f),
                bombCooldown = playerController.CurrentBombCooldown,
                playerPositionOnMap = new List<SimulationStruct>(),
            },
            freePlaces = new List<SimulationStruct>(),
            usedPlaces = new List<SimulationStruct>(),
        };

        var actions = InitAction(_tree.agent);
        _tree.children = ConvertActionToList(_tree, actions);
    }

    private void InitMapArray()
    {
        foreach (var t in GetMapStruct(trackers))
        {
            if (isInSquare(_tree.agent.collider.position ,t.collider))
            {
                _tree.agent.playerPositionOnMap.Add(t);
            }
            
            if (isInSquare(_tree.player.collider.position,t.collider))
            {
                _tree.player.playerPositionOnMap.Add(t);
            }

            if (t.tag == "")
            {
                freePlaces.Add(t);
            }
            else
            {
                usedPlaces.Add(t);
            }
        }
    }

    private Node[] ConvertActionToList(Node parent, int actions)
    {
        Node[] nodes = new Node[actions];

        if (actions == 4)
        {
            nodes[0] = new Node(parent) {state = 'l'};
            nodes[1] = new Node(parent) {state = 'd'};
            nodes[2] = new Node(parent) {state = 'u'};
            nodes[3] = new Node(parent) {state = 'r'};
        }
        else
        {
            nodes[0] = new Node(parent) {state = 'l'};
            nodes[1] = new Node(parent) {state = 'd'};
            nodes[2] = new Node(parent) {state = 'u'};
            nodes[3] = new Node(parent) {state = 'r'};
            nodes[4] = new Node(parent) {state = 'b'};
        }
        for (var i = 0; i < actions; i++)
        {
            InitNode(nodes[i]);
        }

        return nodes;
    }
    private void ProcessSimulation(Node node)
    {
        foreach (var child in node.children)
        {
            agentGlobal = child.parent.agent;
            playerGlobal = child.parent.player;
            freePlaces = child.parent.freePlaces;
            usedPlaces = child.parent.usedPlaces;
            
            for (int k = 0; k < minint; k++)
            {
                int? result = Simulation(child);
                Backpropagation(child, (int) result);
            }
        }
    }

    private void InitNode(Node node)
    {
        node.freePlaces = new List<SimulationStruct>();

        node.parent.freePlaces.ForEach((item) =>
        {
            node.freePlaces.Add(new SimulationStruct(item));
        });
     
        node.usedPlaces = new List<SimulationStruct>();
        
        node.parent.usedPlaces.ForEach((item) =>
        {
            node.usedPlaces.Add(new SimulationStruct(item));
        });

        node.player = node.parent.player;
        node.agent = node.parent.agent;
    }

    private int InitAction(agent agent)
    {
        if (agent.bombCooldown <= 0)
        {
            return 4;
        }
        return 5;
    }

    private Node Selection(Node node)
    {
        Node bestNode = node.children[0];

        foreach (var child in node.children)
        {
            if ( (float) child.score / child.played > (float) bestNode.score / bestNode.played )
            {
                bestNode = child;
            }
        }

        return bestNode;
    }
    
    private int? Simulation(Node node)
    {
        int? result = null;
        ResetMapSimulation(node.parent);

        int test = 0;

        while (result == null)
        {
            ControlBomb();
            RandomAction();
            result = GetResult();
            
            if (test >= 1000 && result == null)
            {
                result = Random.Range(-1, 2);    
            }
            
            test += 1;
        }

        return result;
    }
        
    private void ControlBombPosition(agent agent)
    {
        Vector2 agentPosition = new Vector2(Mathf.Round(agent.position.x), Mathf.Round(agent.position.z));

        var freePlacesLength = freePlaces.Count;
        
        for (var i = 0; i < freePlacesLength; i++)
        {
            if (isInSquare(agentPosition, freePlaces[i].collider))
            {
                var square = freePlaces[i];
                freePlaces[i] = square;
                usedPlaces.Add(freePlaces[i]);
                freePlaces.Remove(freePlaces[i]);

                bombs.Add(new bombStruct()
                {
                    position = new Vector3(Mathf.Round(agent.position.x), 0, Mathf.Round(agent.position.z)),
                    collider = new collider(agentPosition, 1f),
                    explosionColliders = new []
                    {
                        new collider(new Vector2(agentPosition.x + 1, agentPosition.y), 1), // right explosion
                        new collider(new Vector2(agentPosition.x - 1, agentPosition.y), 1), // left explosion
                        new collider(new Vector2(agentPosition.x, agentPosition.y + 1), 1), // up explosion
                        new collider(new Vector2(agentPosition.x, agentPosition.y - 1), 1) // back explosion
                    },
                    timer = 2.0f,
                });

                agent.bombCooldown = bombCooldown;
                return;
            }
        }
    }

    private void ControlBomb()
    {
        if (agentGlobal.bombCooldown > 0)
        {
            agentGlobal.bombCooldown -= interval_between_frame;
        }
        
        if (playerGlobal.bombCooldown > 0)
        {
            playerGlobal.bombCooldown -= interval_between_frame;
        }
        
        if (bombs.Count > 0)
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                var bomb = bombs[i];
                bomb.timer -= interval_between_frame;
                bombs[i] = bomb;
                
                if (bombs[i].timer <= 0f)
                {
                    ControlExplosionBomb(bomb);
                    bombs.Remove(bomb);
                }
            }   
        }
    }

    private void ControlExplosionBomb(bombStruct bomb)
    {
        for (var i = 0; i < bomb.explosionColliders.Length; i++)
        {
            for (var j = 0; j < agentGlobal.playerPositionOnMap.Count; j++)
            {
                if (isInSquare(agentGlobal.playerPositionOnMap[j].collider.position ,bomb.explosionColliders[i]))
                {
                    agentGlobal.alive = false;
                }
            }
            
            for (var j = 0; j < playerGlobal.playerPositionOnMap.Count; j++)
            {
                if (isInSquare(playerGlobal.playerPositionOnMap[j].collider.position ,bomb.explosionColliders[i]))
                {
                    playerGlobal.alive = false;
                }
            }

            for (var j = 0; j < usedPlaces.Count; j++)
            {
                if (isInSquare(bomb.explosionColliders[i].position , usedPlaces[j].collider))
                {
                    freePlaces.Add(usedPlaces[j]);
                    usedPlaces.Remove(usedPlaces[j]);
                }
            }
        }
    }

    private void ControlDeplacements(Vector3 agentDirection, Vector3 playerDirection)
    {
        Vector2 agentPosition = new Vector2(agentGlobal.position.x + agentDirection.x, agentGlobal.position.z + agentDirection.z);
        Vector2 playerPosition = new Vector2(playerGlobal.position.x + playerDirection.x, playerGlobal.position.z + playerDirection.z);

        for (var i = 0; i < freePlaces.Count; i++)
        {
            if (isInSquare(agentPosition, freePlaces[i].collider) || isInSquare(playerPosition, freePlaces[i].collider))
            {
                if (!agentGlobal.playerPositionOnMap.Contains(freePlaces[i]))
                {
                    agentGlobal.playerPositionOnMap.Add(freePlaces[i]);
                    usedPlaces.Add(freePlaces[i]);
                    freePlaces.Remove(freePlaces[i]);
                    agentGlobal.position.x = agentPosition.x;
                    agentGlobal.position.z = agentPosition.y;
                }
                else
                {
                    playerGlobal.playerPositionOnMap.Add(freePlaces[i]);
                    usedPlaces.Add(freePlaces[i]);
                    freePlaces.Remove(freePlaces[i]);
                    playerGlobal.position.x = playerPosition.x;
                    playerGlobal.position.z = playerPosition.y;
                }
            }
        }

        foreach (var bomb in bombs)
        {
            for (var i = 0; i < playerGlobal.playerPositionOnMap.Count; i++)
            {
                if (!isInSquare(playerPosition, playerGlobal.playerPositionOnMap[i].collider))
                {
                    if (!isInSquare(bomb.collider.position, playerGlobal.playerPositionOnMap[i].collider))
                    {
                        freePlaces.Add(playerGlobal.playerPositionOnMap[i]);
                        usedPlaces.Remove(playerGlobal.playerPositionOnMap[i]);
                    }
                    playerGlobal.playerPositionOnMap.Remove(playerGlobal.playerPositionOnMap[i]);
                }
            }
        
            for (var i = 0; i < agentGlobal.playerPositionOnMap.Count; i++)
            {
                if (!isInSquare(agentPosition, agentGlobal.playerPositionOnMap[i].collider))
                {
                    if (!isInSquare(bomb.collider.position, agentGlobal.playerPositionOnMap[i].collider))
                    {
                        freePlaces.Add(agentGlobal.playerPositionOnMap[i]);
                        usedPlaces.Remove(agentGlobal.playerPositionOnMap[i]);
                    }
                    agentGlobal.playerPositionOnMap.Remove(agentGlobal.playerPositionOnMap[i]);   
                }
            }
        }
    }

    private bool isInSquare(Vector2 agent, collider squareCollider)
    {
        if (squareCollider.position.x <= agent.x &&
            squareCollider.position.x + squareCollider.size >= agent.x &&
            squareCollider.position.y <= agent.y &&
            squareCollider.position.y + squareCollider.size >= agent.y)
        {
            return true;
        }

        return false;
    }
    private int? GetResult()
    {
        if (agentGlobal.alive == false && playerGlobal.alive == false) {
            return 0;
        }
        if(agentGlobal.alive && playerGlobal.alive == false) {
            return 1;
        }
        if (agentGlobal.alive == false && playerGlobal.alive) {
            return -1;
        }
        return null;
    }
    private void RandomAction()
    {
        var actionsAgent = InitAction(agentGlobal);
        var actionPlayer = InitAction(playerGlobal);

        Vector3 directionPlayer = new Vector3();
        Vector3 directionAgent = new Vector3();
        
        switch (Random.Range(0, actionPlayer + 1))
        {
            case 0:
                directionPlayer = Vector3.right * (Time.deltaTime * speedPlayer);
                break;
            case 1:
                directionPlayer = Vector3.left * (Time.deltaTime * -speedPlayer);
                break;
            case 2:
                directionPlayer = Vector3.forward * (Time.deltaTime * speedPlayer);
                break;
            case 3:
                directionPlayer = Vector3.back * (Time.deltaTime * -speedPlayer);
                break;
            case 4:
                ControlBombPosition(playerGlobal);
                break;
        }
        
        switch (Random.Range(0, actionsAgent + 1))
        {
            case 0:
                directionAgent = Vector3.right * (Time.deltaTime * speedPlayer);
                break;
            case 1:
                directionAgent = Vector3.left * (Time.deltaTime * -speedPlayer);
                break;
            case 2:
                directionAgent = Vector3.forward * (Time.deltaTime * speedPlayer);
                break;
            case 3:
                directionAgent = Vector3.back * (Time.deltaTime * -speedPlayer);
                break;
            case 4:
                ControlBombPosition(agentGlobal);
                break;
        }

        ControlDeplacements(directionAgent, directionPlayer);
    }
    
    private void Backpropagation(Node node, int result)
    {
        node.played += 1;
        node.score += result;

        if (node.parent != null)
        {
            Backpropagation(node.parent, result);
        }
    }
    
    private void ResetMapSimulation(Node node)
    {
        for (var i = 0; i < node.freePlaces.Count; i++)
        {
            freePlaces[i] = node.freePlaces[i];
        }
        
        for (var i = 0; i < node.usedPlaces.Count; i++)
        {
            usedPlaces[i] = node.usedPlaces[i];
        }
        
    }
    
    private List<SimulationStruct> GetMapStruct(List<TrackerCollider> gameState)
    {
        List<SimulationStruct> simulationStructure = new List<SimulationStruct>();
        
        foreach (var t in gameState)
        {
            var newSimulationStruct = new SimulationStruct(
                t.transform.position,
                t.tagName,
                new collider(t.transform.position, 1f)
            );

            simulationStructure.Add(newSimulationStruct);
        }
        
        return simulationStructure;
    }
}