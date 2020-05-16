using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pianist.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace Pianist.NPCs
{
    [AutoloadHead]
    class GymLover : ModNPC
    {
        //人物图片
        public override string Texture => "Pianist/NPCs/GymLover";
        //人物派对图片
        public override string[] AltTextures => new[] { "Pianist/NPCs/GymLover_Party" };
        public static NPC FindNPC(int npcType) => Main.npc.FirstOrDefault(npc => npc.type == npcType && npc.active);
        public static void UpdateGymLover()
        {
            NPC traveler = FindNPC(NPCType<GymLover>());
            if (traveler == null) {
                int newTraveler = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, NPCType<GymLover>(), 1); // Spawning at the world spawn
                traveler = Main.npc[newTraveler];
                traveler.homeless = true;
                traveler.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
                traveler.netUpdate = true;
            }
            
        }

        public override bool Autoload(ref string name)
        {
            name = "Brother Tie";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brother Tie");
            DisplayName.AddTranslation(GameCulture.Chinese, "铁哥");
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 5;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 30;
            //好战程度 越小越好战
            NPCID.Sets.AttackAverageChance[npc.type] = 5;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 22;
            npc.height = 32;  
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 25;
            npc.lifeMax = 250;
            //受伤音效
            npc.HitSound = SoundID.NPCHit1;
            //死亡音效
            npc.DeathSound = SoundID.NPCDeath1;
            //抗击退性，数字越大抗性越低
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemType<Drumstick>())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            return "Tie";
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            {
                if (!Main.bloodMoon && !Main.eclipse)
                {
                    //无家可归时
                    if (npc.homeless)
                    {
                        chat.Add("小学弟....嘻嘻嘻！");
                    }
                    else
                    {
                        chat.Add("撸他一杆！");
                    }
                }
                return chat;
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "商店";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemType<Drumstick>());
            shop.item[nextSlot].value = 10;
            nextSlot++;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 5;
        }

        //弹幕设置
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.BoneArrow;
            attackDelay = 10;
            //NPC在出招后多长时间才会发射弹幕
        }

        //弹幕的向量相关
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            //射速
            multiplier = 15f;
            gravityCorrection = 0f;
            //攻击精准度
            randomOffset = 3f;
        }
    }
}
