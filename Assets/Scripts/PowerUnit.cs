using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Understeer))]
    [RequireComponent(typeof(Oversteer))]
    public class PowerUnit : MonoBehaviour
    {
        [field: Header("Parameters")]
        [field: Tooltip("Acceleration value of the power unit")]
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: Tooltip("Deceleration value of the power unit")]
        [field: SerializeField] public float Deceleration { get; private set; }
        [field: Tooltip("Turn in value of the power unit")]
        [field: SerializeField] public float TurnIn { get; private set; }
        [field: Tooltip("Top speed value of the power unit")]
        [field: SerializeField] public float TopSpeed { get; private set; }
        
        [Header("Fields")]
        private float rotation;
        private float velocity;

        [Header("References")]
        private Rigidbody2D rb;
        private Understeer understeer;
        private Oversteer oversteer;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            understeer = GetComponent<Understeer>();
            oversteer = GetComponent<Oversteer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {   
            Thrusting();
            Steering();
            Braking();
        }

        private void Thrusting()
        {
            if (Input.GetKey(KeyCode.W))
            {
                // Calculate how much forwards we are going depending on the direction of our velocity
                velocity = Vector2.Dot(transform.up, rb.linearVelocity);

                // Limit the speed of the forward direction of the car till reaching top speed
                if (velocity > TopSpeed)
                    return;

                // Create thrust for the power unit
                Vector2 thrust = Acceleration * Time.deltaTime * transform.up;

                // Apply force and push the car
                rb.AddForce(thrust, ForceMode2D.Force);
            }
        }

        private void Braking()
        {
            if (Input.GetKey(KeyCode.S))
            {
                // Calculate how much forwards we are going depending on the direction of our velocity
                velocity = Vector2.Dot(transform.up, rb.linearVelocity);

                // Limit the speed of the backwards direction of the car till reaching absolute zero
                if (velocity >= 0)
                    return;
                
                // Create thrust for the brakes
                Vector2 thrust = Deceleration * Time.deltaTime * -transform.up;
                
                // Apply force and stop the car
                rb.AddForce(thrust, ForceMode2D.Force);
            }
        }

        private void Steering()
        {   
            if (Input.GetKey(KeyCode.A))
            {   
                // Rotate the car to the left
                rotation += TurnIn / understeer.Amount * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {   
                // Rotate the car to the right
                rotation -= TurnIn / understeer.Amount * Time.deltaTime;
            }
            
            // Rotate the car using the rigidbody
            rb.MoveRotation(rotation);
        }
    }
}