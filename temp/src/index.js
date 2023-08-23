
webgazer.setTracker("TFFacemesh"); //set a tracker module

webgazer.setRegression("weightedRidge");

webgazer.removeMouseEventListeners()

catscol = ["gray","brown","white"]
fishcol = ["gray","brown","white"]
birdcol = ["yellow","blue","brown"]
itemcolors = [catscol,fishcol,birdcol]
animals = ["cat","fish","bird"]
bColor = ["white","green","purple"]

class Example extends Phaser.Scene
{
	
    constructor ()
    {
        super();
		this.money = 1000;
		this.itemsTypes = [];
		this.itemsSprits = []; 
		this.itemsText = [];
		this.itemsValue = [];
		this.itemsGRate = [];
		this.sold = []
		this.moneyText = null;
    }

    preload ()
    {
        this.load.image('sold', './../assets/sold.png');
		this.load.spritesheet('items', './../assets/ItemsSheet.png', { frameWidth: 128, frameHeight: 128 });
    }

    create ()
    {	
		webgazer.setGazeListener(this.gazeCallback).begin()
		var x = 250;
		var y = 100; 
		
		this.moneyText = this.add.text(900, 40, this.money+ "$", { font: '30px Courier', fill: '#00FF00' });

		
        for (let i = 0; i < 3; i++) {
			for (let j = 0; j < 4; j++) {
				var r = Phaser.Math.Between(0, 26);
				while (this.itemsTypes.includes(r)){
					r = Phaser.Math.Between(0, 26);
				}
				
				this.itemsSprits.push(this.add.image(x, y, 'items', r))
				this.itemsTypes.push(r)
				var v =  Phaser.Math.Between(30, 100);
				this.itemsGRate.push(Phaser.Math.FloatBetween(1.05, 1.1))
				this.itemsValue.push(v)
				this.itemsText.push(this.add.text(x - 55, y + 37, v + "$", { font: '20px Courier', fill: '#FFFFFFFF' }))
				this.sold.push(1)

				x += 188;
			}
			x = 250
			y += 188;
		}
		var v = Phaser.Math.Between(1, 11)
		var target = this.itemsTypes[v];
		
		console.log(v,target,this.itemsValue[v])
		console.log(target/9)
		console.log(target%9)
		console.log((target%9)/3, (target%9)%3)
		

		this.add.text(900,300,
			"Animal: " + animals[(target%9)%3]+"\n" +
			"color : " + itemcolors[(target%9)%3][Math.floor((target%9)/3)] + "\n"
			+ "type  : " + bColor[Math.floor(target/9)]  ,{ font: '16px Courier', fill: '#00FF00' })

        this.input.on('pointerup', this.mouseDown);
    }

	mouseDown() {
		var mx = game.input.mousePointer.x;
		var my = game.input.mousePointer.y;
		let x = 250
		let y = 100
		var index = 0;
		for (let i = 0; i < 3; i++) {
			if (Math.abs(y - my) <= 64 ) {
				for (let j = 0; j < 4; j++) {
					if (Math.abs(x - mx) <= 64 ) {
						if (this.scene.sold[index] == 1){
							this.scene.sold[index] = 0
							this.scene.money = this.scene.money - this.scene.itemsValue[index];
							this.scene.add.image(x, y, 'sold')
							this.scene.moneyText.setText(this.scene.money + "$")
							console.log(index);
						}
						break
					}
					index += 1;
					x += 188;
				}
				break;
			}
			index += 4;
			y += 188;
		}
	}

	gazeCallback(data){
		if (data == null) {
			return;
		}
		let x = 250
		let y = 100
		var index = 0;
		for (let i = 0; i < 3; i++) {
			if (Math.abs(y - data.y) <= 64 ) {
				for (let j = 0; j < 4; j++) {
					if (Math.abs(x - data.x) <= 64 ) {
						if (game.scene.scenes[0].sold[index] == 1){
							game.scene.scenes[0].itemsValue[index] = Math.round(game.scene.scenes[0].itemsValue[index] * game.scene.scenes[0].itemsGRate[index])
							game.scene.scenes[0].itemsText[index].setText( game.scene.scenes[0].itemsValue[index] + "$")
							console.log(index);
						}

						break
					}
					index += 1;
					x += 188;
				}
				break;
			}
			index += 4;
			y += 188;
		}
	}
}

const config = {
    type: Phaser.WEBGL,
    parent: 'phaser-example',
    width: 1200,
    height: 700,
	backgroundColor: "#323c39",
    scene: Example
};

const game = new Phaser.Game(config);
