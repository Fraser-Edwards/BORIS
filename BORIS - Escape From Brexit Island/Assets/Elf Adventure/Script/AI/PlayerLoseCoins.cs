using UnityEngine;
using System.Collections;


[RequireComponent(typeof(GiveDamageToPlayer))]
public class PlayerLoseCoins : MonoBehaviour {

	[Header("Lose Coins (percentage)")]
    [Range (0,100)]
    public int CoinsToLose = 50;
    public GameObject LoseCoinFx;
    [Tooltip("delay a moment before give next damage to Player")]
	public float rateCoinLoss = 0.5f;

    public AudioClip loseCoinSfx;
    float nextCoinLoss;

    private GiveDamageToPlayer gdp;

	void OnTriggerStay2D(Collider2D other)
    {
        gdp = GetComponent<GiveDamageToPlayer>();
        var Player = other.GetComponent<Player> ();
		if (Player == null)
			return;

		if (!Player.isPlaying)
			return;

		if (Time.time < nextCoinLoss + rateCoinLoss)
			return;

        nextCoinLoss = Time.time;

		if (CoinsToLose == 0)
			return;

        if(gdp.canBeKillOnHead && Player.transform.position.y > transform.position.y)
            return;

        Player.LoseCoins(CoinsToLose);

        if (LoseCoinFx != null) Instantiate(LoseCoinFx, GameManager.Instance.Player.transform.position, Quaternion.identity);
        SoundManager.PlaySfx(loseCoinSfx);
    }
}
