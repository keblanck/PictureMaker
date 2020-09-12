 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class pictureMaker : MonoBehaviour {

	private Texture2D myPicture;
	private float[] rgbVal;
	private float[] newRgbVal;
	private string[] entries;
	public int width;
	public int height;
	public float maxColor;
	public string loadFile = "";
	public string saveFile = "";

	//public static bool Button;


	void readFile(string textInput) {
		//with this method you read the file all at once, if the file is huge, this would not be ideal
		string filename = "/Users/kathleenblanck/Documents/comp325/lab1/sampleinputs/" + textInput;
		string allText = System.IO.File.ReadAllText(filename);
		string replaceText = Regex.Replace(allText,"[^a-zA-Z0-9%._]"," ");

		//replace all non-word characters with spaces 
		entries = replaceText.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);

		//split by spaces, not including empty strings 
		width = int.Parse(entries[1]);
	
		// these should be global variables...
		height = int.Parse(entries[2]);
		myPicture = new Texture2D((int)(width),(int)(height));

		//global 
		maxColor = int.Parse(entries[3]); 
		int w = 0;
		int h = height - 1;
		rgbVal = new float[width * height * 3 + 4];
		for (int i = 4; i < (width * height * 3); i+=3) {
			rgbVal[i] = float.Parse(entries[i])/maxColor;
			rgbVal[i + 1] = float.Parse(entries[i + 1])/maxColor;
			rgbVal[i + 2] = float.Parse(entries[i + 2])/maxColor;
			Color c = new Color (rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);

			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}
		myPicture.Apply ();
	}

	void writeFile (string saveFile) {
		string newPic = entries [0];
		newPic += "\n";
		for (int i = 1; i < 4; i++) {
			newPic += entries [i];
			newPic += "\n";
		}

		newRgbVal = new float[(width * height * 3) + 4];

		for (int i = 4; i < ((width * height * 3) + 4); i++) {
			newRgbVal [i] = rgbVal [i] * 255;
			newPic += newRgbVal [i];
			newPic += " ";
		}

		string filename = "/Users/kathleenblanck/Documents/comp325/lab1/sampleinputs/" + saveFile;
		System.IO.File.WriteAllText(filename, newPic);
	}
		
	void Awake() {
		//readFile ();
	}
	void OnGUI() {
		//special adjustment to center picture
		//loadFile.inputType = InputField.InputType.Standard;
		GUI.DrawTexture(new Rect((Screen.width/2) - (width/2), (Screen.height/2) - (height/2), width, height), myPicture);


		loadFile = GUI.TextField(new Rect(10, 10, 200, 20), loadFile);

		if (GUI.Button(new Rect(10, 35, 200, 20), "Load Picture")) {
			readFile (loadFile);
		}
		if (GUI.Button (new Rect (10, 60, 150, 20), "negate red")) {
			negate_red ();
		}
		if (GUI.Button (new Rect (10, 85, 150, 20), "negate green")) {
			negate_green ();
		}
		if (GUI.Button (new Rect (10, 110, 150, 20), "negate blue")) {
			negate_blue ();
		}
		if (GUI.Button (new Rect (10, 135, 150, 20), "flatten red")) {
			flatten_red ();
		}
		if (GUI.Button (new Rect (10, 160, 150, 20), "flatten green")) {
			flatten_green ();
		}
		if (GUI.Button (new Rect (10, 185, 150, 20), "flatten blue")) {
			flatten_blue ();
		}
		if (GUI.Button (new Rect (10, 210, 150, 20), "turn grey")) {
			grey_scale ();
		}
		if (GUI.Button (new Rect (10, 235, 150, 20), "flip horizontal")) {
			flip_horizontal ();
		}
		if (GUI.Button (new Rect (10, 260, 150, 20), "maximize contrast")) {
			extreme_contrast ();
		}

		saveFile = GUI.TextField(new Rect(10, 285, 200, 20), saveFile);
		if (GUI.Button(new Rect(10, 310, 200, 20), "Save Picture")) {
			writeFile (saveFile);
		}
	}

	void negate_red() {
		int w = 0;
		int h = height - 1;
		for (int i = 4; i < (width * height * 3); i+=3) {
			rgbVal [i] = 1 - rgbVal [i];
			Color c = new Color(rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void negate_green() {
		int w = 0;
		int h = height - 1;
		for (int i = 5; i < (width * height * 3); i+=3) {
			rgbVal [i] = 1 - rgbVal [i];
			Color c = new Color(rgbVal[i - 1], rgbVal[i], rgbVal[i + 1]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void negate_blue() {
		int w = 0;
		int h = height - 1;
		for (int i = 6; i < (width * height * 3); i+=3) {
			rgbVal [i] = 1 - rgbVal [i];
			Color c = new Color(rgbVal[i - 2], rgbVal[i - 1], rgbVal[i]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void flatten_red() {
		int w = 0;
		int h = height - 1;
		for (int i = 4; i < (width * height * 3); i+=3) {
			rgbVal [i] = 0;
			Color c = new Color(rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void flatten_green() {
		int w = 0;
		int h = height - 1;
		for (int i = 5; i < (width * height * 3); i+=3) {
			rgbVal [i] = 0;
			Color c = new Color(rgbVal[i - 1], rgbVal[i], rgbVal[i + 1]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void flatten_blue() {
		int w = 0;
		int h = height - 1;
		for (int i = 6; i < (width * height * 3); i+=3) {
			rgbVal [i] = 0;
			Color c = new Color(rgbVal[i - 2], rgbVal[i - 1], rgbVal[i]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void grey_scale() {
		int w = 0;
		int h = height - 1;
		float sum;
		for (int i = 4; i < (width * height * 3); i+=3) {
			sum = rgbVal [i];
			sum += rgbVal [i + 1];
			sum += rgbVal [i + 2];
			sum /= 3;

			rgbVal [i] = sum;
			rgbVal [i + 1] = sum;
			rgbVal [i + 2] = sum;
			Color c = new Color(rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void flip_horizontal() {
		int w = 0, wn = 2;
		float temp;
		int h = height - 1, hn = 1;
	
		for (int i = 4; i < width * height * 3; i += 3) { 
			temp = rgbVal [i];
			rgbVal [i] = rgbVal [(hn * width * 3) - wn];
			rgbVal [(hn * width * 3) - wn] = temp;

			temp = rgbVal [i + 1];
			rgbVal [i + 1] = rgbVal [(hn * width * 3) - (wn - 1)];
			rgbVal [(hn * width * 3) - (wn - 1)] = temp;

			temp = rgbVal [i + 2];
			rgbVal [i + 2] = rgbVal [(hn * width * 3) - (wn - 2)];
			rgbVal [(hn * width * 3) - (wn - 2)] = temp;

			wn+=3;
			if (wn > ((width * 3) / 2)) {
				i = 4 + (width * hn * 3);
				hn++;
				wn = 2;
			}
		}
			

		for (int i = 4; i < (width * height * 3); i+=3) {
			Color c = new Color(rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

	void extreme_contrast () {
		int w = 0;
		int h = height - 1;
		for (int i = 4; i < (width * height * 3); i+=3) {
			if (rgbVal [i] < .5) {
				rgbVal [i] = 0;
			} else {
				rgbVal [i] = 1;
			}
			if (rgbVal [i + 1] < .5) {
				rgbVal [i + 1] = 0;
			} else {
				rgbVal [i + 1] = 1;
			}
			if (rgbVal [i + 2] < .5) {
				rgbVal [i + 2] = 0;
			} else {
				rgbVal [i + 2] = 1;
			}
			Color c = new Color(rgbVal[i], rgbVal[i + 1], rgbVal[i + 2]);
			myPicture.SetPixel (w, h, c);
			w++;
			if (w == width) {
				h--;
				w = 0;
			}
		}

		myPicture.Apply ();
	}

}

