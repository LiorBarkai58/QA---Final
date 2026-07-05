    using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlaymodeTests
{
    private Enemy enemy;
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return SceneManager.LoadSceneAsync("Test_Scene");
        enemy = Object.FindFirstObjectByType<Enemy>();
    }

    [UnityTest]
    public IEnumerator KillEnemyTest()
    {
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
        if (Player.instance)
        {
            enemy.transform.position = Player.instance.transform.position;
            yield return new WaitForSeconds(0.1f);
        }

        Assert.IsFalse(Player.instance);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyShootProjectile()
    {
        Transform projectile = enemy.ForceShoot();
        projectile.position = Player.instance.transform.position;
        yield return new WaitForSeconds(1);
        Assert.IsFalse(Player.instance);
        
    }
}
