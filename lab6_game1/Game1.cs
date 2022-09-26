using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace lab6_game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D Char;
        Vector2 charPos = new Vector2(0, 250);
        Texture2D ball;
        Vector2 [] ballPos = new Vector2[4];
      
        int[] ballColor = new int[4];

        int direction = 0;
        KeyboardState keyboardState;
        KeyboardState old_keyboardstate;

        int frame;
        int totalFrames;
        int framePerSec;
        float timePerFrame;
        float totalElapsed;
        int speed = 5;
        bool personHit;

        Random r = new Random();

        //55
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Char = Content.Load<Texture2D>("Char01 (1)");
            ball = Content.Load<Texture2D>("ball");
            frame = 0;
            totalFrames = 4;
            framePerSec = 12;
            timePerFrame = (float)1 / framePerSec;
            totalElapsed = 0;

            for (int i = 0; i < 4; i++)
            {
                ballPos[i].X = r.Next(0, _graphics.GraphicsDevice.Viewport.Width - ball.Width/6);
                ballPos[i].Y = r.Next(0, _graphics.GraphicsDevice.Viewport.Height - ball.Height );
                ballColor[i] = r.Next(0, 6);

            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S) )
            {
                charPos.Y += speed;
                direction = 0;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            }
            if (keyboardState.IsKeyDown(Keys.D) )
            {
                charPos.X += speed;
                direction = 2;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (keyboardState.IsKeyDown(Keys.A) )
            {
                charPos.X -= speed;
                direction = 1;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboardState.IsKeyDown(Keys.W) )
            {
                charPos.Y -= speed;
                direction = 3;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            old_keyboardstate = keyboardState;

            Rectangle charRectangle = new Rectangle((int)charPos.X, (int)charPos.Y, 32, 48);
          
           for(int i=0; i <4; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)ballPos[i].X, (int)ballPos[i].Y, 24, 24);

                if (charRectangle.Intersects(blockRectangle) == true)
                {
                    ballPos[i].X = r.Next(0, _graphics.GraphicsDevice.Viewport.Width - ball.Width / 6);
                    ballPos[i].Y = r.Next(0, _graphics.GraphicsDevice.Viewport.Height - ball.Height);
                    ballColor[i] = r.Next(0, 6);
                }

                else if (charRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }

           

            
            // tehe

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            if(personHit == true)
            {
                device.Clear(Color.Red);

            }
            else
            {
                device.Clear(Color.CornflowerBlue);
            }

            _spriteBatch.Begin();

            _spriteBatch.Draw(Char, charPos, new Rectangle(32 * frame, 48 * direction, 32, 48), Color.White);
            

            for(int i = 0; i <4; i++)
            {
                _spriteBatch.Draw(ball, ballPos[i], new Rectangle(24 * ballColor[i], 0, 24, 24), Color.White);
            }

            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame = (frame + 1) % totalFrames;
                totalElapsed -= timePerFrame;
            }
        }
    }
}
