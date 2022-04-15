using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;


    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]  // REQUIRES A COLLIDER2D to function
    // 1. Attach this to a read/write enabled sprite image
    // 2. Set the drawing_layers  to use in the raycast
    // 3. Attach a 2D collider (like a Box Collider 2D) to this sprite
    // 4. Hold down left mouse to draw on this texture!
    public class Drawable : MonoBehaviour
    {
        // PEN COLOUR
        public static Color Pen_Colour = Color.red;     // Change these to change the default drawing settings
        // PEN WIDTH (actually, it's a radius, in pixels)
        public static int Pen_Width = 3;
        public string Path;

        float spread;


        public delegate void Brush_Function(Vector2 world_position);
        // This is the function called when a left click happens
        // Pass in your own custom one to change the brush type
        // Set the default function in the Awake method
        public Brush_Function current_brush;

        public LayerMask Drawing_Layers;

        public bool Reset_Canvas_On_Play = true;
        // The colour the canvas is reset to each time
        public Color Reset_Colour = new Color(0, 0, 0, 0);  // By default, reset the canvas to be transparent

        // Used to reference THIS specific file without making all methods static
        public static Drawable drawable;
        // MUST HAVE READ/WRITE enabled set in the file editor of Unity
        Sprite drawable_sprite;
        Texture2D drawable_texture;

        Vector2 previous_drag_position;
        Color[] clean_colours_array;
        Color transparent;
        Color32[] cur_colors;
        bool mouse_was_previously_held_down = false;
        bool no_drawing_on_current_drag = false;



//////////////////////////////////////////////////////////////////////////////
// BRUSH TYPES. Implement your own here


        // When you want to make your own type of brush effects,
        // Copy, paste and rename this function.
        // Go through each step
        public void BrushTemplate(Vector2 world_position)
        {
            // 1. Change world position to pixel coordinates
            Vector2 pixel_pos = WorldToPixelCoordinates(world_position);

            // 2. Make sure our variable for pixel array is updated in this frame
            cur_colors = drawable_texture.GetPixels32();

            ////////////////////////////////////////////////////////////////
            // FILL IN CODE BELOW HERE

            // Do we care about the user left clicking and dragging?
            // If you don't, simply set the below if statement to be:
            //if (true)

            // If you do care about dragging, use the below if/else structure
            if (previous_drag_position == Vector2.zero)
            {
                // THIS IS THE FIRST CLICK
                // FILL IN WHATEVER YOU WANT TO DO HERE
                // Maybe mark multiple pixels to colour?
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
            }
            else
            {
                // THE USER IS DRAGGING
                // Should we do stuff between the previous mouse position and the current one?
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);
            }
            ////////////////////////////////////////////////////////////////

            // 3. Actually apply the changes we marked earlier
            // Done here to be more efficient
            ApplyMarkedPixelChanges();
            
            // 4. If dragging, update where we were previously
            previous_drag_position = pixel_pos;
        }


        public void Bucket(Vector2 world_position)
        {
            // 1. Change world position to pixel coordinates
            Vector2 pixel_pos = WorldToPixelCoordinates(world_position);

            // 2. Make sure our variable for pixel array is updated in this frame
            cur_colors = drawable_texture.GetPixels32();
            flood_fill(pixel_pos);
            ApplyMarkedPixelChanges();
            
        }

        public void PenBrush(Vector2 world_point)
        {
            Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

            cur_colors = drawable_texture.GetPixels32();
            spread = 1;
            if (previous_drag_position == Vector2.zero)
            {
                // If this is the first time we've ever dragged on this image, simply colour the pixels at our mouse position
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
            }
            else
            {
                // Colour in a line from where we were on the last update call
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);
            }
            ApplyMarkedPixelChanges();

            //Debug.Log("Dimensions: " + pixelWidth + "," + pixelHeight + ". Units to pixels: " + unitsToPixels + ". Pixel pos: " + pixel_pos);
            previous_drag_position = pixel_pos;
        }
    
        public void Gomme(Vector2 world_point)
        {
            Vector2 pixel_pos = WorldToPixelCoordinates(world_point);
            spread = 1;
            cur_colors = drawable_texture.GetPixels32();
            Color nullColor = new Color(0,0,0,0);
            if (previous_drag_position == Vector2.zero)
            {
                MarkPixelsToColour(pixel_pos, Pen_Width,nullColor );
            }
            else
            {
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, nullColor);
            }
            ApplyMarkedPixelChanges();
            previous_drag_position = pixel_pos;
        }

        public void Crayon(Vector2 world_point)
        {
            Vector2 pixel_pos = WorldToPixelCoordinates(world_point);
            Color crayonColor = new Color(Pen_Colour.r,Pen_Colour.g,Pen_Colour.b,0.1f);
            cur_colors = drawable_texture.GetPixels32();
            spread = 0.4f;
            if (previous_drag_position == Vector2.zero)
            {
                MarkPixelsToColour(pixel_pos, Pen_Width,crayonColor);
            }
            else
            {
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, crayonColor);
            }
            ApplyMarkedPixelChanges();
            previous_drag_position = pixel_pos;
        }

        public void SetOutilToGomme()
        {
            // PenBrush is the NAME of the method we want to set as our current brush
            current_brush = Gomme;
        }


        public void SetThickness(float thickness)
        {
            Pen_Width = (int) (20*thickness);
        }

        public void SetOutilToMarqueur()
        {
            current_brush = PenBrush;
        }

        public void SetOutilToBucket()
        {

            current_brush = Bucket;
        }

        public void SetOutilToCrayon()
        {

            current_brush = Crayon;
        }


        public void changeColorToBlue()
        {
            Pen_Colour = Color.blue;
        }
        public void changeColorToRed()
        {
            Pen_Colour = Color.red;
        }
        public void changeColorToYellow()
        {
            Pen_Colour = Color.yellow;
        }
        public void changeColorToOrange()
        {
            Pen_Colour = new Color(249f/255,101f/255,21f/255,1);
        }
        public void changeColorToVert()
        {
            Pen_Colour = Color.green;
        }
        public void changeColorToViolet()
        {
            Pen_Colour = new Color(199f/255,36f/255,177f/255,1);
        }
        public void changeColorToNoir()
        {
            Pen_Colour = Color.black;
        }
        public void changeColorToCyan()
        {
            Pen_Colour = Color.cyan;
        }
//////////////////////////////////////////////////////////////////////////////






        // This is where the magic happens.
        // Detects when user is left clicking, which then call the appropriate function
        void Update()
        {
            // Is the user holding down the left mouse button?
            bool mouse_held_down = Input.GetMouseButton(0);
            if (mouse_held_down && !no_drawing_on_current_drag)
            {
                // Convert mouse coordinates to world coordinates
                Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Check if the current mouse position overlaps our image
                Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);
                if (hit != null && hit.transform != null)
                {
                    // We're over the texture we're drawing on!
                    // Use whatever function the current brush is
                    current_brush(mouse_world_position);
                }

                else
                {
                    // We're not over our destination texture
                    previous_drag_position = Vector2.zero;
                    if (!mouse_was_previously_held_down)
                    {
                        // This is a new drag where the user is left clicking off the canvas
                        // Ensure no drawing happens until a new drag is started
                        no_drawing_on_current_drag = true;
                    }
                }
            }
            // Mouse is released
            else if (!mouse_held_down)
            {
                previous_drag_position = Vector2.zero;
                no_drawing_on_current_drag = false;
            }
            mouse_was_previously_held_down = mouse_held_down;
        }

        public void flood_fill(Vector2 mypoint)
        {
            int x = (int) mypoint.x;
            int y=(int) mypoint.y;
            int array_pos = y * (int)drawable_sprite.rect.width + x;
            if (array_pos > cur_colors.Length || array_pos < 0)
                return;
            Color oldcolor=cur_colors[array_pos];
            if (oldcolor==Pen_Colour)
                return;
            Queue<KeyValuePair<int,int>> myqueue = new Queue<KeyValuePair<int, int>>();
            myqueue.Enqueue(new KeyValuePair<int, int>(x,y));
            while ((myqueue.Count != 0))
            {   

                KeyValuePair<int,int> currentvalue=myqueue.Dequeue();
                x=currentvalue.Key;
                y=currentvalue.Value;
                array_pos = y * (int)drawable_sprite.rect.width + x;

                if (array_pos >= cur_colors.Length || array_pos < 0 || (cur_colors[array_pos]!=oldcolor && cur_colors[array_pos].a==255 ))
                    continue;
                else {
                    cur_colors[array_pos]=new Color(Pen_Colour.r,Pen_Colour.g,Pen_Colour.b,1);
                    myqueue.Enqueue(new KeyValuePair<int, int>(x+1,y));
                    myqueue.Enqueue(new KeyValuePair<int, int>(x-1,y));
                    myqueue.Enqueue(new KeyValuePair<int, int>(x,y+1));
                    myqueue.Enqueue(new KeyValuePair<int, int>(x,y-1));

                }
            }

            
        }

        // Set the colour of pixels in a straight line from start_point all the way to end_point, to ensure everything inbetween is coloured
        public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
        {
            // Get the distance from start to finish
            float distance = Vector2.Distance(start_point, end_point);
            Vector2 direction = (start_point - end_point).normalized;

            Vector2 cur_position = start_point;

            // Calculate how many times we should interpolate between start_point and end_point based on the amount of time that has passed since the last update
            float lerp_steps = 1 / distance;

            for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
            {
                cur_position = Vector2.Lerp(start_point, end_point, lerp);
                MarkPixelsToColour(cur_position, width, color);
            }
        }





        public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            // Figure out how many pixels we need to colour in each direction (x and y)
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;
            //int extra_radius = Mathf.Min(0, pen_thickness - 2);
            for (int x = center_x - 2*pen_thickness ; x <= center_x + 2*pen_thickness; x++)
            {
                // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
                if (x >= (int)drawable_sprite.rect.width || x < 0)
                    continue;

                for (int y = center_y - 2*pen_thickness; y <= center_y + 2*pen_thickness; y++)
                {
                    int x_rel = x - center_x;
                    int y_rel = y -center_y;
                    float distanceToCenter =  Mathf.Sqrt(x_rel*x_rel + y_rel*y_rel);
                    
                    float random = Random.Range(0f,1f);
                   if (distanceToCenter <= (2*pen_thickness) && random<=spread)
                    {
                        MarkPixelToChange(x, y, color_of_pen,distanceToCenter,pen_thickness);
                    }
                }
            }
        }
        public static Color CombineColors(Color color1, Color color2)
        {
        Color result = new Color(Mathf.Min(color1.r ,color2.r ),Mathf.Min(color1.g, color2.g ),Mathf.Min(color1.b, color2.b ),255);
        return result;
        }

        public static float MixColors(float indice1,float indice2,float distanceToCenter,int pen_thickness)
        {
            return ((2-distanceToCenter/pen_thickness)*indice1+(1-(2-distanceToCenter/pen_thickness))*indice2);
        }
        
        public static float MixColorsWithAlpha(float indice1,float indice2,float alpha1)
        {
            return (alpha1*indice1+(1-alpha1)*indice2);
        }
        public static Color AntiAliasing(Color color1,Color color2,float distanceToCenter,int pen_thickness)
        {
            Color result;
            if(distanceToCenter<pen_thickness){
                if(color2.a==0)
                    result = new Color(color1.r,color1.g,color1.b,Mathf.Min(1,color2.a+color1.a));
                else
                    result = new Color(MixColorsWithAlpha(color1.r,color2.r,color1.a),MixColorsWithAlpha(color1.g,color2.g,color1.a),MixColorsWithAlpha(color1.b,color2.b,color1.a),Mathf.Min(1,color2.a+color1.a));

            } else {
                if(color2.a==0)
                    result = new Color(color1.r,color1.g,color1.b,Mathf.Max((2-distanceToCenter/pen_thickness)*color1.a,color2.a));
                else 
                {
                    float newr = MixColorsWithAlpha(MixColors(color1.r,color2.r,distanceToCenter,pen_thickness),color2.r,color1.a);
                    float newg = MixColorsWithAlpha(MixColors(color1.g,color2.g,distanceToCenter,pen_thickness),color2.g,color1.a);
                    float newb = MixColorsWithAlpha(MixColors(color1.b,color2.b,distanceToCenter,pen_thickness),color2.b,color1.a);

                    result = new Color(newr,newg,newb,Mathf.Max((2-distanceToCenter/pen_thickness)*color1.a,color2.a));
                }
            }
            
            
            return result;
        }

        float compute_alpha(float color1alpha, float color2alpha){
            return Mathf.Min(1,color1alpha+color2alpha);
        }
        
        public void MarkPixelToChange(int x, int y, Color color,float distanceToCenter,int pen_thickness)
        {
            // Need to transform x and y coordinates to flat coordinates of array
            int array_pos = y * (int)drawable_sprite.rect.width + x;

            // Check if this is a valid position
            if (array_pos > cur_colors.Length || array_pos < 0)
                return;

            if(color.a!=0)
                cur_colors[array_pos] = AntiAliasing(color,cur_colors[array_pos],distanceToCenter,pen_thickness);
            else 
                cur_colors[array_pos] =color;
            
        }
        public void ApplyMarkedPixelChanges()
        {
            drawable_texture.SetPixels32(cur_colors);
            drawable_texture.Apply();
        }


        // Directly colours pixels. This method is slower than using MarkPixelsToColour then using ApplyMarkedPixelChanges
        // SetPixels32 is far faster than SetPixel
        // Colours both the center pixel, and a number of pixels around the center pixel based on pen_thickness (pen radius)
        public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            // Figure out how many pixels we need to colour in each direction (x and y)
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;
            //int extra_radius = Mathf.Min(0, pen_thickness - 2);

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    drawable_texture.SetPixel(x, y, color_of_pen);
                }
            }

            drawable_texture.Apply();
        }


        public Vector2 WorldToPixelCoordinates(Vector2 world_position)
        {
            // Change coordinates to local coordinates of this image
            //Vector3 local_pos = transform.InverseTransformPoint(world_position);
            Vector3 local_pos = new Vector3(world_position.x,world_position.y,0);
            // Change these to coordinates of pixels
            float pixelWidth = drawable_sprite.rect.width;
            float pixelHeight = drawable_sprite.rect.height;
            float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;

            // Need to center our coordinates
            float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }


        // Changes every pixel to be the reset colour
        public void ResetCanvas()
        {
            drawable_texture.SetPixels(clean_colours_array);
            drawable_texture.Apply();
            SetOutilToCrayon();
        }

        public void SaveCanvas()
        {
            byte[] bytes = drawable_texture.EncodeToPNG();
            File.WriteAllBytes(Path, bytes);

        }

        public void LoadCanvas()
        {
            if (File.Exists(Path)){
                byte[] fileData = File.ReadAllBytes(Path);
                drawable_texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }

        }
        
        void Awake()
        {
            drawable = this;
            // DEFAULT BRUSH SET HERE
            current_brush = PenBrush;
            Path=Application.persistentDataPath + "/../test.png";
            drawable_sprite = this.GetComponent<SpriteRenderer>().sprite;
            drawable_texture = drawable_sprite.texture;

            // Initialize clean pixels to use
            clean_colours_array = new Color[(int)drawable_sprite.rect.width * (int)drawable_sprite.rect.height];
            for (int x = 0; x < clean_colours_array.Length; x++)
                clean_colours_array[x] = Reset_Colour;
            // Should we reset our canvas image when we hit play in the editor?
            if (Reset_Canvas_On_Play)
                ResetCanvas();
        }
    }
