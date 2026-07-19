using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditMode
{
    [Test]
    public void TestDamageLogic()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.GetDamage(1);
        
        Assert.AreEqual(enemy.health, 9);
        Object.DestroyImmediate(enemy);
        
    }
    // A Test behaves as an ordinary method
    [Test]
    public void TestShieldLogicScenario1()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.shield = 1;
        enemy.GetDamage(1);
        
        Assert.AreEqual(enemy.health, 10);
        
        Object.DestroyImmediate(enemy);
    }
    
    [Test]
    public void SetShieldScenario1()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.SetShield(7);
        
        Assert.AreEqual(enemy.shield, 7);
        
        Object.DestroyImmediate(enemy);
    }
    [Test]
    public void SetShieldScenario2()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.SetShield(-5);
        
        Assert.AreEqual(enemy.shield, 0);
        
        Object.DestroyImmediate(enemy);
    }
    
    
    [Test]
    public void TestShieldLogicScenario2()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.SetShield(7);
        enemy.GetDamage(1);
        
        Assert.AreEqual(enemy.health, 10);
        Assert.AreEqual(enemy.shield, 6);
        
        Object.DestroyImmediate(enemy);
    }
    
    [Test]
    public void TestShieldLogicScenario3()
    {
        var go = new GameObject("EnemyTest");
        var enemy = go.AddComponent<Enemy>();

        enemy.health = 10;
        enemy.SetShield(-5);
        enemy.GetDamage(1);
        
        Assert.AreEqual(enemy.health, 9);
        Assert.AreEqual(enemy.shield, 0);
        
        Object.DestroyImmediate(enemy);
    }



    
}
