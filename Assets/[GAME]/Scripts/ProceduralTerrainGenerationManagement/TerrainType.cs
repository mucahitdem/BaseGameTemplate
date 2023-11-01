﻿using System;
using UnityEngine;

namespace Scripts.GameScripts.ProceduralTerrainGenerationManagement
{
    [Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}