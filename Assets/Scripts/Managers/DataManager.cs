using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class DataManager : IManager
    {
        public Dictionary<string, Digimon> Digimons { get; private set; }

        public void Init()
        {
            var json = GameManager.Resources.GetResources<TextAsset>("Data/digimon");
            var rawDict = JsonConvert.DeserializeObject<Dictionary<string, Digimon>>(json.text);
            foreach (var kvp in rawDict)
            {
                kvp.Value.id = kvp.Key;
            }

            Digimons = rawDict;
            Debug.Log(Digimons["abbadomon"].name_kor);
        }

        public Digimon GetRandomDigimon()
        {
            var list=Digimons.Where(x => x.Value.level == "성장기").Select(x=>x.Value).ToList();
            var index = Random.Range(0, list.Count);
            return list[index];
        }
    }

    [Serializable]
    public class Digimon
    {
        public string id;
        public string name_kor;
        public string image_url;
        public string level;
        public string type;
        public string attribute;
        public List<string> skills;
        public string profile;
    }
}