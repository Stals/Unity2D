
public class Player{
	float money;
	public Player(float _money){
		money = _money;
	}

	public float getMoney()
	{
		return money;
	}

	public void substractMoney(float m){
		money -= m;
	}

	public void addMoney(float m){
		money += m;
	}

}