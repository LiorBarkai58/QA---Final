    using System.Collections;
using NUnit.Framework;
using Space_Shooter_Template_FREE.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlaymodeTests
{
    private Enemy enemy;
    private Enemy boss;
    
    private TestScenarioReferences testReferences;
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return SceneManager.LoadSceneAsync("Test_Scene");
        testReferences =Object.FindFirstObjectByType<TestScenarioReferences>(); 
        enemy = testReferences.enemy;
        boss = testReferences.boss;
    }

    [UnityTest]
    public IEnumerator KillEnemyTest()
    {
        EnemySetup();
        if (PlayerShooting.instance)
        {
            PlayerShooting.instance.MakeAShot();
            yield return new WaitForSeconds(0.1f);
            PlayerShooting.instance.MakeAShot();
            yield return new WaitForSeconds(0.1f);
            PlayerShooting.instance.MakeAShot();

            yield return new WaitForSeconds(1);
        }

        Assert.IsFalse(enemy);
        yield return null;
    }
    [UnityTest]
    public IEnumerator EnemyOnPlayer()
    {
        EnemySetup();
        if (Player.instance)
        {
            enemy.transform.position = Player.instance.transform.position;
            yield return new WaitForSeconds(0.1f);
        }

        Assert.IsFalse(Player.instance);
        yield return null;
    }

    [UnityTest]
    public IEnumerator FireAtShield()
    {
        EnemySetup();
        enemy.SetShield(10000);
        PlayerShooting.instance.MakeAShot();
        yield return new WaitForSeconds(1);
        
        Assert.Greater(enemy.shield, 0);
    }

    [UnityTest]
    public IEnumerator EnemyShootProjectile()
    {
        EnemySetup();
        Transform projectile = enemy.ForceShoot();
        projectile.position = Player.instance.transform.position;
        yield return new WaitForSeconds(1);
        Assert.IsFalse(Player.instance);
        
    }
    
    [UnityTest]
    public IEnumerator BossTest()
    {
        BossSetup();
        Transform projectile = boss.ForceShoot();
        projectile.position = Player.instance.transform.position;
        yield return new WaitForSeconds(1);
        Assert.IsFalse(Player.instance);
        
    }

    private void BossSetup()
    {
        Object.DestroyImmediate(enemy.gameObject);
        
    }

    private void EnemySetup()
    {
        Object.DestroyImmediate(boss.gameObject);
    }

    private void NoEnemySetup()
    {
        Object.DestroyImmediate(enemy.gameObject);
        Object.DestroyImmediate(boss.gameObject);
        
    }



    [UnityTest]
    public IEnumerator PowerupTest()
    {
        Bonus bonus = Object.FindFirstObjectByType<Bonus>();
        int startingPower = PlayerShooting.instance.weaponPower;
        bonus.transform.position = Player.instance.transform.position;
        yield return new WaitForSeconds(1);
        Assert.AreEqual(PlayerShooting.instance.weaponPower, startingPower+1);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator TestDeathLogic()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.GetDamage(10);
        yield return null;
        Assert.IsFalse(enemy);
    }

    [UnityTest]
    public IEnumerator TestWaveSpawn()
    {
        NoEnemySetup();
        testReferences.normalWavePrefab.count = 1;
        testReferences.normalWavePrefab.timeBetween = 1;
        testReferences.normalWavePrefab.shieldHP = 3;
        Wave wave = Object.Instantiate(testReferences.normalWavePrefab);

        yield return null;
        Enemy enemy = Object.FindFirstObjectByType<Enemy>();
        Assert.IsTrue(enemy);//Found enemy spawned by wave
        Assert.AreEqual(enemy.shield, testReferences.normalWavePrefab.shieldHP);

        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Assert.AreEqual(1 , enemies.Length);
        yield break;
    }
    
    

    
}
