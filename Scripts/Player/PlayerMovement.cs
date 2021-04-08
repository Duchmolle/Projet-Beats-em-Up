using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Show In Inspector
    [SerializeField] Rigidbody2D _playerRigidbody;
    [SerializeField] float _movementSpeed;
    [SerializeField] TransformData _playerTransformData;
    [SerializeField] float _jumpHeight;
    #endregion

    #region Variables Globales
    Vector2 _movementInputs;
    float _movementQuantity;
    Transform _playerTransform;
    public bool _isJump;
    #endregion

    #region Public Properties
    public float DirectionX
    {
        get
        {
            return _playerRigidbody.velocity.x;
        }
    }

    public float DirectionY
    {
        get
        {
            return _playerRigidbody.velocity.y;
        }
    }
    #endregion

    #region Public Methods
    private void GetInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movementInputs = new Vector2(horizontal, vertical).normalized;
        _movementQuantity = Mathf.Clamp01(_movementInputs.magnitude);
        _isJump = Input.GetButtonDown("Jump");
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            GetComponentInChildren<Transform>().localScale = new Vector2(Mathf.Sign(_playerRigidbody.velocity.x), 1);

        }
    }



    #endregion

    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        //Condition pour éviter une erreur lors des essais avec la TransformData
        if (_playerTransformData != null)
        {
            _playerTransformData.value = _playerTransform;
        }

        GetInputs();

        

    }

    private void FixedUpdate()
    {
        _playerRigidbody.velocity = _movementInputs * _movementQuantity * _movementSpeed;

        FlipSprite();

        Jump();
    }

    private void Jump()
    { 

    }
        
}
