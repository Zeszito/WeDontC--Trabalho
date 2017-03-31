using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FrontDirection.GraphicsSupport;

namespace FrontDirection
{
    class ChaserGameObject : GameObject
    {
        //Target;
        protected TexturedPrimitive mTarget;
        //Verificador se houve contacto;
        protected bool mHitTarget;
        //Velocidade do target;
        protected float mHomeInRate;


        //VARIAVEIS PARA SEREM CHAMADAS FORA DA CLASS
        public float HomeInRate { get { return mHomeInRate; }set { mHomeInRate = value; } }
        public bool HitTarget {get { return mHitTarget; } }
        public bool HasValidTarget { get { return null != mTarget; } }
        public TexturedPrimitive Target
        {
            get { return mTarget; }
            set
            {
                mTarget = value;
                mHitTarget = false;
                if(null != mTarget)
                {
                    FrontDirection = mTarget.Position - Position;
                    VelocityDirection = FrontDirection;
                }
            }
        }


        public ChaserGameObject(String imageName, Vector2 position, Vector2 size, TexturedPrimitive target) 
            : base(imageName, position, size, null)
        {
            mTarget = target;
            mHomeInRate = 0.05f;
            mHitTarget = false;
            mSpeed = 0.1f;
        }
        public void ChaseTarget()
        {
            #region Step 4a.
            if (null == mTarget) return;
            base.Update();
            #endregion
            #region Step4b.
            mHitTarget = PrimitivesTouches(mTarget);
            if (!mHitTarget)
            {
                #region Calculate angle
                Vector2 targetDir = mTarget.Position - Position;
                float distToTargetSq = targetDir.LengthSquared();

                targetDir /= (float)Math.Sqrt(distToTargetSq);
                float cosTheta = Vector2.Dot(FrontDirection, targetDir);
                float theta = (float)Math.Acos(cosTheta);
                #endregion
                #region Calculate rotation direction
                
                if(theta > float.Epsilon)
                {
                    Vector3 fIn3D = new Vector3(FrontDirection, 0f);
                    Vector3 tIn3D = new Vector3(targetDir, 0f);
                    Vector3 sign = Vector3.Cross(tIn3D, fIn3D);

                    RotateAngleInRadian *= Math.Sign(sign.Z) * theta * mHomeInRate;
                    VelocityDirection = targetDir;//mTarget.Position -Position;
                    
                }
                
                #endregion
            }
            #endregion
        }
    }
}

