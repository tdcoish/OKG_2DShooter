/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PC_Cont : MonoBehaviour
{
    private Rigidbody2D                     cRigid;
    private PC_Gun                          cGun;
    private PC_Grnd                         cGrnd;
    
    public float                            _spd;
    public float                            _maxHealth = 1000f;
    public float                            _health;

    public UI_PC                            rUI;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>(); 
        cGun = GetComponent<PC_Gun>();   
        cGrnd = GetComponent<PC_Grnd>();

        _health = _maxHealth;
    }

    void Update()
    {
        cRigid.velocity = HandleInputForVel();
        RotateToMouse();
        cGun.FRun();
        cGrnd.FRun();
        CheckDead();
        rUI.FSetBarSize(_health/_maxHealth);
        rUI.FSetAmmoBarSize(cGun._ammo, cGun._maxAmmo);
    }

    private Vector3 HandleInputForVel()
    {
        Vector2 vVel = new Vector2();
        if(Input.GetKey(KeyCode.A)){
            vVel.x -= _spd;
        }
        if(Input.GetKey(KeyCode.D)){
            vVel.x += _spd;
        }
        if(Input.GetKey(KeyCode.W)){
            vVel.y += _spd;
        }
        if(Input.GetKey(KeyCode.S)){
            vVel.y -= _spd;
        }
        return vVel;
    }

    private void RotateToMouse(){
		Camera c = Camera.main;
		Vector2 msPos = c.ScreenToWorldPoint(Input.mousePosition);

		Vector2 distance = msPos - (Vector2)transform.position;
		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.eulerAngles = new Vector3(0, 0, angle);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EN_Melee>()){
            _health -= 35f;
        }

        if(other.GetComponent<PCK_Health>()){
            _health += 50f;
            if(_health > _maxHealth){
                _health = _maxHealth;
            }
        }
        if(other.GetComponent<PCK_Ammo>()){
            cGun._ammo += 25;
            if(cGun._ammo > cGun._maxAmmo){
                cGun._ammo = cGun._maxAmmo;
            }
        }
        if(other.GetComponent<EX_Grenade>()){
            _health -= 50f;
        }
        if(other.GetComponent<PJ_Base>()){
            _health -= 60f;
        }
    }

    private void OnColliderEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PJ_Bolt>()){
            _health -= 60f;
        }

        if(other.gameObject.GetComponent<ENV_Border>()){
            cRigid.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<EN_Lava>()){
            _health -= Time.deltaTime * 20f;
        }
    }

    private void CheckDead()
    {
        if(_health <= 0f){
            TDC_EventManager.FBroadcast(TDC_GE.GE_PCDeath);
        }
    }
}
