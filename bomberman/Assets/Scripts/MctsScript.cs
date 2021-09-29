using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    internal struct SimulationStruct
    {
        public Vector3 position;
    }
    internal struct bombStruct
    {
        public Vector3 position;
    }

    internal struct colliderSimulator
    {
        public float size;

        public colliderSimulator(float size)
        {
            this.size = size;
        }
    }

    internal struct agent
    {
        public Vector3 position;
        public colliderSimulator collider;
        public List<bombStruct> bombs;
        public float lastBombPlantedTimer;
        public bool alive;
    }
    internal class Node
    {
        public Node parent;
        public Node[] children;
        public int played;
        public int won;
        public agent player;
        public agent agent;
        public char state;

        public List<SimulationStruct> stateSimulationGame;
        public Node(Node parent)
        {
            player = new agent();
            agent = new agent();
            this.parent = parent;
            played = 0;
            won = 0;
        }
    }
    
    // 2 scds
    
    public class MctsScript : MonoBehaviour
    {
        private float INTERVAL_BETWEEN_FRAME = 1 / Time.deltaTime;
        private float BOMB_DELAY = 2.0f;
        private float timePassed = 0.0f;
        
        private Node _tree;
        private Vector3 agentPositionInit;
        private Vector3 playerPositionInit;
        private List<SimulationStruct> mapSimulation;
        private float startSimulation;
        List<bombStruct> bombs;
        
        [SerializeField]
        private int minint;

        public void InitAgentPosition(Vector3 agentPosition, Vector3 playerPosition)
        {
            agentPositionInit = agentPosition;
            playerPositionInit = playerPosition;
        }
        
        public void GetNextMove(List<BoxScript> gameState)
        {
            startSimulation = Time.time;

            _tree = new Node(null)
            {
                stateSimulationGame = GetMapStruct(gameState),
                state = 'i',
                player =
                {
                    position = playerPositionInit,
                    bombs = new List<bombStruct>(),
                    alive = true,
                    collider = new colliderSimulator(0.4f)
                },
                agent =
                {
                    position = agentPositionInit,
                    bombs = new List<bombStruct>(),
                    alive = true,
                    collider = new colliderSimulator(0.4f)
                }
            };



            mapSimulation = new List<SimulationStruct>(_tree.stateSimulationGame);
            
            var actions = InitAction(_tree.agent);
            _tree.children = ConvertActionToList(_tree, actions);

            Node node = _tree;
            // make depth test

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    ProcessSimulation(node);
                }
                else
                {
                    node = Selection(node);
                    ProcessSimulation(node);
                }
            }
        }

        private Node[] ConvertActionToList(Node parent, char[] actions)
        {
            Node[] nodes = new Node[actions.Length];

            for (var i = 0; i < actions.Length; i++)
            {
                nodes[i] = new Node(parent);
                nodes[i].state = actions[i];
            }

            return nodes;
        }
        private void ProcessSimulation(Node node)
        {
            foreach (var child in node.children)
            {
                for (int k = 0; k < minint; k++)
                {
                    Node result = Simulation(child);
                    Backpropagation(result);
                }
            }
        }

        private void InitNode(Node node)
        {
            node.stateSimulationGame = new List<SimulationStruct>(node.parent.stateSimulationGame);
            node.player.position = playerPositionInit;
            node.player.bombs = new List<bombStruct>();
            node.player.alive = true;
            node.player.collider = new colliderSimulator(0.4f);
            
            node.agent.position = agentPositionInit;
            node.agent.bombs = new List<bombStruct>();
            node.agent.alive = true;
            node.agent.collider = new colliderSimulator(0.4f);
        }

        private char[] InitAction(agent agent)
        {
            char[] actions;

            if (timePassed - agent.lastBombPlantedTimer < BOMB_DELAY)
            {
                actions = new char[5];
            }
            else
            {
                actions = new char[4];
            }

            for(var i = 0; i < actions.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        actions[i] = 'r';
                        break;
                    case 1:
                        actions[i] = 'l';
                        break;
                    case 2:
                        actions[i] = 'u';
                        break;
                    case 3:
                        actions[i] = 'd';
                        break;
                    case 4:
                        actions[i] = 'b';
                        break;
                }
            }

            return actions;
        }

        private Node Selection(Node node)
        {
            Node bestNode = node.children[0];

            foreach (var child in node.children)
            {
                if ((child.won / child.played) > (bestNode.won / bestNode.played ))
                {
                    bestNode = child;
                }
            }

            return bestNode;
        }

        private void Expansion(Node node)
        {
            if (node.children == null)
            {
                // InitAction(node);   
            }
        }

        private Node SelectRandomlyNode(Node node)
        {
            Node child = node.children[Random.Range(0, 5)];
            child.stateSimulationGame = new List<SimulationStruct>(node.parent.stateSimulationGame);
            
            child.player.position = playerPositionInit;
            child.player.bombs = new List<bombStruct>();
            child.player.alive = true;
            child.player.collider = new colliderSimulator(0.4f);
            
            child.agent.position = agentPositionInit;
            child.agent.bombs = new List<bombStruct>();
            child.agent.alive = true;
            child.agent.collider = new colliderSimulator(0.4f);
            
            Expansion(child);
            return child;
        }
        private Node Simulation(Node node)
        {
            int? result = null;
            Node resutlNode = node;

            while (result == null)
            {
                resutlNode = SelectRandomlyNode(resutlNode);
                RandomAgentAction(resutlNode);
                RandomActionPlayer(resutlNode);
                ControlState(resutlNode);
                result = GetResult(resutlNode);
            }
            
            return resutlNode;
        }

        private void ControlState(Node node)
        {
        }
        private void RandomActionPlayer(Node node)
        {
            var actions = InitAction(node.agent);
            
            switch (actions[Random.Range(0, actions.Length)])
            {
                case 'r':
                    node.agent.position += Vector3.right * (float) (Time.deltaTime * 1.5);
                    break;
                case 'l':
                    node.agent.position -= Vector3.right * (float) (Time.deltaTime * 1.5);
                    break;
                case 'u':
                    node.agent.position += Vector3.forward * (float) (Time.deltaTime * 1.5);
                    break;
                case 'd':
                    node.agent.position -= Vector3.forward * (float) (Time.deltaTime * 1.5);
                    break;
                case 'b':
                    if (node.agent.bombs.Count < 1)
                    {
                        node.agent.bombs.Add(new bombStruct()); 
                    }
                    break;
            }
        }
        private int? GetResult(Node node)
        {
            if (node.agent.alive == false && node.player.alive == false) {
                return 0;
            }
            if(node.agent.alive && node.player.alive == false) {
                return 1;
            }
            if (node.agent.alive == false && node.player.alive) {
                return -1;
            }

            return null;
        }
        private void RandomAgentAction(Node node)
        {
            var actions = InitAction(node.agent);
            
            switch (actions[Random.Range(0, actions.Length)])
            {
                case 'r':
                    node.agent.position += Vector3.right * (float) (Time.deltaTime * 1.5);
                    break;
                case 'l':
                    node.agent.position -= Vector3.right * (float) (Time.deltaTime * 1.5);
                    break;
                case 'u':
                    node.agent.position += Vector3.forward * (float) (Time.deltaTime * 1.5);
                    break;
                case 'd':
                    node.agent.position -= Vector3.forward * (float) (Time.deltaTime * 1.5);
                    break;
                case 'b':
                    if (node.agent.bombs.Count < 1)
                    {
                        node.agent.bombs.Add(new bombStruct()); 
                    }
                    break;
            }
        }

        private void ControlDeplacements(float deplacement)
        {
            
        }
        private List<SimulationStruct> GetMapStruct(List<BoxScript> gameState)
        {
            List<SimulationStruct> simulationStructure = new List<SimulationStruct>();

            foreach (var t in gameState)
            {
                var simulationStruct = new SimulationStruct();
                simulationStruct.position = t.currentPositionBox;
                simulationStructure.Add(simulationStruct);
            }
            
            return simulationStructure;
        }
        private void Backpropagation(Node node)
        {
            node.played += 1;
            //
            // if (win)
            // {
            //     node.won += 1;
            // }
            //
            // if (node.parent != null)
            // {
            //     Backpropagation(node.parent, win);
            // }
        }

        // private void ControlBomb()
        // {
        //     foreach (var bomb in bombs)
        //     {
        //         if () // timer === 0
        //         {
        //             
        //         }
        //     }
        // }
    }
    
}
