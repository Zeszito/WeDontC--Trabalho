using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BookExample;
using FirstGame.GraphicSupport;
using static FirstGame.GraphicSupport.Font;

namespace FirstGame.GraphicSupport
{
    public class BasketBall : TexturedPrimitive
    {
        // Change current position by this amount
        private const float kIncreaseRate = 1.001f;
        private Vector2 kInitSize = new Vector2(5, 5);
        private Vector2 Lol = new Vector2(2, 3);
        private const float kFinalSize = 15f;
        
        public BasketBall(): base("BasketBall",InputWrapper.ThumbSticks.Left,InputWrapper.ThumbSticks.Right)
        {
            mPosition = Camera.RandomPosition();
            mSize = kInitSize;
        }
        public bool UpdateAndExplode()
        {
            mSize *= kIncreaseRate;
            return mSize.X > kFinalSize;
        }
    }
} 