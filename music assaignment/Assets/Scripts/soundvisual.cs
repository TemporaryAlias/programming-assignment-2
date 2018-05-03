using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundvisual : MonoBehaviour {

    [SerializeField] private float rmsvalue;
    [SerializeField] private float dbvalue;
    [SerializeField] private float pitch;
    [SerializeField] private float visualModifier = 50.0f;
    [SerializeField] private float smoothspeed = 10.0f;
    [SerializeField] private float maxvisualheight = 10f;
    [SerializeField] private float keeppercentage = 0.5f;
    [SerializeField] private float backgroundintensity;
    [SerializeField] private Quaternion movespeed;
    
    const int sound = 1024;

    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float samplerate;
    private int audioscale = 10;


    private Transform[] visuallist;
    private float[] visualscale;
    

    public Material background1,background2,background3,background4,background5,background6;
    public Color mincol1,mincol2,mincol3,mincol4,mincol5,mincol6;
    public Color maxcol1,maxcol2,maxcol3,maxcol4,maxcol5,maxcol6;
    public GameObject thing;
    
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
		source.clip = SongManager.currentSong.clip;
		source.Play ();

        samples = new float[sound];
        spectrum = new float[sound];
        samplerate = AudioSettings.outputSampleRate;
      
        spawnshape();
    }

    public void spawnshape()
    { var radius = 3;
        
        visualscale = new float[audioscale];
        visuallist = new Transform[audioscale];
        
        for(var i = 0; i < audioscale; i++)
        { var angle = i * Mathf.PI * 2 / audioscale;
            var pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius ;

            GameObject shiza = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;
            visuallist[i] = shiza.transform;
            visuallist[i].position = pos;

          
            
        }
      
    }

    // Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneChanger sceneChanger = GetComponent<SceneChanger> ();

			sceneChanger.LoadSceneByInt (0);
		}

        analyzesound();
        updatevisual();
        updatebackground();
	}

    private void analyzesound()
    {
        source.GetOutputData(samples, 0);
           //getrms value
           float sum = 0;
            for(int i =0; i < sound; i++)
        {
            sum = samples[i] * samples[i];
        }
        rmsvalue = Mathf.Sqrt(sum / sound);

        // get db value
        dbvalue = 20 * Mathf.Log10(rmsvalue / 0.1f);

        //get sound spectrum
        source.GetSpectrumData(spectrum,0, FFTWindow.BlackmanHarris);

        //find pitch 
        float maxV = 0;
        var maxN = 0;
        for (int i = 0; i<sound; i++)
        {
            if (!(spectrum[i] > maxV) || !(spectrum[i] > 0.0f))
                continue;
            maxV = spectrum[i];
            maxN = i;
        }
        float freqN = maxN;
        if(maxN > 0 && maxN <sound - 1)
        {
            var dl = spectrum[maxN - 1] / spectrum[maxN];
            var dr = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dr * dr - dl * dl);
        }
        pitch = freqN * (samplerate / 2) / sound;

    }

    private void updatevisual()
    {
        movespeed = new Quaternion(1, 1, 1, 1);
        int visualindex = 0;
        int spectrumindex = 0;
        int averagesize =(int) ((sound*keeppercentage) / audioscale);

        while (visualindex < audioscale)
        {
            int j = 0;
            float sum = 0;
            while (j < averagesize)
            {
                sum += spectrum[spectrumindex];
                spectrumindex++;
                j++;
            }
            float scaleY = sum / averagesize * visualModifier;
            if (visuallist[visualindex].position.y>maxvisualheight)
            {
                var maxheight = new Vector3(visuallist[visualindex].position.x, maxvisualheight, visuallist[visualindex].position.z);
                visuallist[visualindex].transform.position = maxheight;
            }


            visualscale[visualindex] -= Time.deltaTime * smoothspeed;


            if (visualscale[visualindex] < scaleY)
                visualscale[visualindex] = scaleY;

            var axis = new Vector3(1, 1, 1);
            var thething = new Vector3(thing.transform.position.x,thing.transform.position.y,thing.transform.position.z);
            var change = new Vector3(visuallist[visualindex].transform.position.x, visualscale[visualindex], visuallist[visualindex].transform.position.z);
            visuallist[visualindex].position = change;
            
            
            visualindex++;
            
        }
    }

    private void updatebackground()
    {
        backgroundintensity = Time.deltaTime * 0.01f;
        if (backgroundintensity > dbvalue/40)
        {
            backgroundintensity = dbvalue/40;
           
          background1.color = Color.Lerp(mincol1, maxcol1, -backgroundintensity);
          background2.color = Color.Lerp(mincol2, maxcol2, -backgroundintensity);
          background3.color = Color.Lerp(mincol3, maxcol3, -backgroundintensity);
          background4.color = Color.Lerp(mincol4, maxcol4, -backgroundintensity);
          background5.color = Color.Lerp(mincol5, maxcol5, -backgroundintensity);
          background6.color = Color.Lerp(mincol6, maxcol6, -backgroundintensity);
        }
    }

   /*private void movement()
    { movespeed +=1*Time.deltaTime;
       int visualindex = 0;
        var movement = new Vector3(Mathf.Cos(movespeed), 0, Mathf.Sin(movespeed))*3;
        visuallist[visualindex].transform.position += (movement * Time.deltaTime);
        visualindex++;
    }*/
}
