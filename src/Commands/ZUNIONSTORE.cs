﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BeetleX.Redis.Commands
{
    public class ZUNIONSTORE : Command
    {
        public ZUNIONSTORE(string key, AggregateType type, params (string key, double weight)[] items)
        {
            Key = key;
            Type = type;
            Items = items;
        }

        public AggregateType Type { get; private set; }

        public (string key, double weight)[] Items { get; private set; }

        public string Key { get; private set; }

        public override bool Read => false;

        public override string Name => "ZUNIONSTORE";

        public enum AggregateType
        {
            SUM,
            MIN,
            MAX
        }

        public override void OnExecute()
        {
            base.OnExecute();
            AddText(Key);
            AddText(Items.Length);
            foreach (var item in Items)
            {
                AddText(item.key);
            }
            AddText("WEIGHTS");
            foreach (var item in Items)
            {
                if (item.weight == 0)
                    AddText(1);
                else
                    AddText(item.weight);
            }
            AddText("AGGREGATE");
            AddText(Type);

        }


    }
}
