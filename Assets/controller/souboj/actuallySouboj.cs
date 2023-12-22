using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class actuallSouboj : MonoBehaviour
{
    [SerializeField]
    UkladaniProEnemy enemyVSouboji;
    [SerializeField]
    UkladaniProHrac hracDt;
    prirazeniDoBoje boj;
    float enemyAtkMulti;
    int enemyDefenseMulti;

    float hracAtkMulti;
    int hracDefenseMulti;
    // Start is called before the first frame update
    void Awake()
    {
        boj = gameObject.GetComponent<prirazeniDoBoje>();
        
    }
    public void Pridej()
    {
       
        hracDt = boj.hracDt;
        enemyVSouboji = boj.enemyVSouboji;
        if(enemyVSouboji.jeNaTahu)
        {
            EnemyBoj();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!enemyVSouboji.jeNaTahu)
        {
            
            Bojuj();
        }
        else
        {
            EnemyBoj();
        }

    }
    // ---------------------------------------------
    #region hrac

    private void Bojuj()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Utoc();
            Debug.Log("útok hráè");
            Debug.Log(enemyVSouboji.zivoty + " " + enemyVSouboji.def + "enemy");
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BranSe();
            Debug.Log("Defense hráè");
            

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ZvisAtk();
            Debug.Log("buff hráè");
            

        }
        

    }
    

    private void Utoc()
    {
        if (enemyVSouboji.def - (hracDt.atk * hracAtkMulti) <= 0)
        {
            enemyVSouboji.zivoty += (int)(enemyVSouboji.def - (hracDt.atk * hracAtkMulti));
            enemyVSouboji.def = 0;
        }
        else
            enemyVSouboji.def -= (hracDt.atk * hracAtkMulti);

        enemyVSouboji.jeNaTahu = true;

    }

    public void BranSe()
    {
        hracDefenseMulti += 5;
        enemyVSouboji.jeNaTahu = true;

    }
    public void ZvisAtk()
    {
        hracAtkMulti += (float)0.5;
        enemyVSouboji.jeNaTahu = true;

    }

    #endregion

    //---------------------------------------------
    #region enemy
    public void EnemyBoj()
    {
        int rnd = UnityEngine.Random.Range(0, 101);
        if (rnd <= 20)
        {
            BranSeEnemy();
            Debug.Log("štíty");
        }
        if (rnd > 20 && rnd < 50)
        {
            ZvisAtkEnemy();
            Debug.Log("buff");
        }
        if (rnd > 49)
        {
            UtocEnemy();
            Debug.Log(hracDt.zivoty + " " + hracDt.def + "hraè");
            Debug.Log("útok");
        }
        enemyVSouboji.jeNaTahu = false;
    }

    private void UtocEnemy()
    {
        if (hracDt.def - (enemyVSouboji.atk * enemyAtkMulti) <= 0)
        {
            hracDt.zivoty += (int)(hracDt.def - (enemyVSouboji.atk * enemyAtkMulti));
            hracDt.def = 0;
        }
        else
            hracDt.def -= (enemyVSouboji.atk * enemyAtkMulti);
    }

    public void BranSeEnemy()
    {
        enemyDefenseMulti += 5;
    }
    public void ZvisAtkEnemy()
    {
        enemyAtkMulti += (float)0.5;

    }
    #endregion
}
