package com.jagex.runescape;

public final class SpotAnimation {

    public static void load(Archive streamLoader)
    {
        Stream stream = new Stream(streamLoader.getFile("spotanim.dat"));
        int count = stream.getUnsignedLEShort();
        if(cache == null)
            cache = new SpotAnimation[count];
        for(int anim = 0; anim < count; anim++)
        {
            if(cache[anim] == null)
                cache[anim] = new SpotAnimation();
            cache[anim].id = anim;
            cache[anim].loadDefinition(stream);
        }

    }

    private void loadDefinition(Stream stream)
    {
        do
        {
            int attribute = stream.getUnsignedByte();
            if(attribute == 0)
                return;
            if(attribute == 1)
                modelId = stream.getUnsignedLEShort();
            else
            if(attribute == 2)
            {
                animationId = stream.getUnsignedLEShort();
                if(AnimationSequence.animations != null)
                    sequences = AnimationSequence.animations[animationId];
            } else
            if(attribute == 4)
                scaleXY = stream.getUnsignedLEShort();
            else
            if(attribute == 5)
                scaleZ = stream.getUnsignedLEShort();
            else
            if(attribute == 6)
                rotation = stream.getUnsignedLEShort();
            else
            if(attribute == 7)
                modelLightFalloff = stream.getUnsignedByte();
            else
            if(attribute == 8)
                modelLightAmbient = stream.getUnsignedByte();
            else
            if(attribute >= 40 && attribute < 50)
                originalModelColours[attribute - 40] = stream.getUnsignedLEShort();
            else
            if(attribute >= 50 && attribute < 60)
                modifiedModelColours[attribute - 50] = stream.getUnsignedLEShort();
            else
                System.out.println("Error unrecognised spotanim config code: " + attribute);
        } while(true);
    }

    public Model getModel()
    {
        Model model = (Model) modelCache.get(id);
        if(model != null)
            return model;
        model = Model.getModel(modelId);
        if(model == null)
            return null;
        for(int colour = 0; colour < 6; colour++)
            if(originalModelColours[0] != 0)
                model.recolour(originalModelColours[colour], modifiedModelColours[colour]);

        modelCache.put(model, id);
        return model;
    }

    private SpotAnimation()
    {
        anInt400 = 9;
        animationId = -1;
        originalModelColours = new int[6];
        modifiedModelColours = new int[6];
        scaleXY = 128;
        scaleZ = 128;
    }

    private final int anInt400;
    public static SpotAnimation cache[];
    private int id;
    private int modelId;
    private int animationId;
    public AnimationSequence sequences;
    private final int[] originalModelColours;
    private final int[] modifiedModelColours;
    public int scaleXY;
    public int scaleZ;
    public int rotation;
    public int modelLightFalloff;
    public int modelLightAmbient;
    public static MRUNodes modelCache = new MRUNodes(30);
}