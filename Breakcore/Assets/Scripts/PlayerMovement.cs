using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ledgeLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake(){
        body=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        boxCollider=GetComponent<BoxCollider2D>();
    }
    private void Update(){
        float horizontalInput=Input.GetAxis("Horizontal");
        
        body.velocity=new Vector2(body.velocity.x,body.velocity.y);
        float horizontalVelocity=body.velocity.x;
        float verticalVelocity=body.velocity.y;
        horizontalVelocity=speed*horizontalInput;

        if(horizontalInput>0.01f&&horizontalVelocity<speed){
            transform.localScale=Vector3.one;
        }
        else if(horizontalInput<-0.01f&&horizontalVelocity>-speed){
            transform.localScale=new Vector3(-1,1,1);
        }
        body.velocity=new Vector2(horizontalVelocity,body.velocity.y);
        if(Input.GetKey(KeyCode.W)&&isGrounded()){
            Jump();
        }
        anim.SetBool("run",(horizontalInput!=0&&onWall()==false));
        anim.SetBool("grounded",isGrounded());
    }

    private void Jump(){
        if(isGrounded()){
            body.velocity=new Vector2(body.velocity.x,jumpPower);
            anim.SetTrigger("jump");
        }
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit=Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider!=null;
    }

    private bool onLedge(){
        RaycastHit2D raycastHit=Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,ledgeLayer);
        return raycastHit.collider!=null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit=Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
        return raycastHit.collider!=null;
    }

    private bool isRunning(){
        return false;
    }

    private bool isChangingDirection(){
        return false;
    }

    public bool isMovingOnImpact(){
        return false;
    }
}
