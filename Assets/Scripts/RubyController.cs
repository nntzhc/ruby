using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed;//Ruby的速度

    //Ruby生命值
    public int maxHealth = 5;//最大生命值
    private int currentHealth;//Ruby的当前生命值
    public int Health { get { return currentHealth; } }

    //Ruby的无敌时间
    public float timeInvincible = 2.0f;//无敌时间常量
    public bool isInvincible;
    public float invincibleTimer;//计时器

    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;

    public GameObject projectilePrefab;

    public AudioSource audioSource;

    public AudioSource walkAudioSource;

    public AudioClip playerHit;
    public AudioClip attackSoundClip;
    public AudioClip walkSound;

    private Vector3 respawnPosition;
    private bool isRuneUIOpen = false;
    public GameObject runePanel;
    private Vector2 position;

    // Start is called before the first frame update
    private void Start()
    {
        //Application.targetFrameRate = 30;
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        //currentHealth = 3;
        //speed = 4;
        //int a = GetRubyHealthValue();
        //Debug.Log("Ruby当前的血量是："+a);
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //玩家输入监听
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        //当前玩家输入的某个轴向值不为0
        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            //lookDirection = move;
            lookDirection.Normalize();
            // if (!walkAudioSource.isPlaying)
            // {
            //     walkAudioSource.clip = walkSound;
            //     walkAudioSource.Play();
            // }
        }
        else
        {
            // walkAudioSource.Stop();
        }
        //动画的控制
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //移动
        position = transform.position;
        ////Ruby的水平方向移动
        //position.x = position.x + speed * horizontal*Time.deltaTime;
        ////Ruby的垂直方向移动
        //position.y = position.y + speed * vertical*Time.deltaTime;
        //Ruby位置的移动
        position = position + speed * move * Time.deltaTime;
        // transform.position = position;
        rigidbody2d.MovePosition(position);

        //无敌时间计算
        if (isInvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }
        //修理机器人的方法（攻击）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        //检测是否与NPC对话
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position +
                Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCDialog npcDialog = hit.collider.GetComponent<NPCDialog>();
                if (npcDialog != null)
                {
                    npcDialog.DisplayDialog();
                }
            }
        }
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     if (!isRuneUIOpen)
        //     {
        //         Debug.Log("set active" + runePanel);
        //         runePanel.SetActive(true);
        //         isRuneUIOpen = true;
        //     }
        //     else
        //     {
        //         runePanel.SetActive(false);
        //         isRuneUIOpen = false;
        //     }
        // }

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            //收到伤害
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(playerHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //Debug.Log(currentHealth+"/"+maxHealth);

        if (currentHealth <= 0)
        {
            Respawn();
        }

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    //public int GetRubyHealthValue()
    //{
    //    return currentHealth;
    //}

    private void Launch()
    {
        // if (!UIHealthBar.instance.hasTask)
        // {
        //     return;
        // }
        GameObject projectileObject = Instantiate(projectilePrefab,
            rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(position, lookDirection, 10, 4);
        animator.SetTrigger("Launch");
        PlaySound(attackSoundClip);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition;
    }
}
