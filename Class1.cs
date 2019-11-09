using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using Font = GTA.Font;
using System.IO;
using System.Linq;
using System.Threading.Tasks;




public class RobFleeca : Script
{
    private ScriptSettings KeysData;

    Driller Drill;

    List<Rob> Robs = new List<Rob>();

    List<Vector3> Bank_Locations = new List<Vector3>();

    List<Vector3> EnterExitBank_Locations = new List<Vector3>();

    List<Vector3> Cam1_Locations = new List<Vector3>();
    List<Vector3> Cam2_Locations = new List<Vector3>();
    List<Vector3> Cam3_Locations = new List<Vector3>();
    List<Vector3> Cam4_Locations = new List<Vector3>();
    List<Vector3> Cam5_Locations = new List<Vector3>();

    List<float> Cam1_Heading = new List<float>();
    List<float> Cam2_Heading = new List<float>();
    List<float> Cam3_Heading = new List<float>();
    List<float> Cam4_Heading = new List<float>();
    List<float> Cam5_Heading = new List<float>();

    List<Vector3> Teller_Locations = new List<Vector3>();
    List<float> Teller_Heading = new List<float>();

    List<Vector3> Hostage1_Locations = new List<Vector3>();
    List<float> Hostage1_Heading = new List<float>();

    List<Vector3> Hostage2_Locations = new List<Vector3>();
    List<float> Hostage2_Heading = new List<float>();

    List<Vector3> Hostage3_Locations = new List<Vector3>();
    List<float> Hostage3_Heading = new List<float>();

    List<Vector3> Teller_Walk_Locations1 = new List<Vector3>();
    List<Vector3> Teller_Walk_Locations2 = new List<Vector3>();

    List<Vector3> FleecaDoor_Locations = new List<Vector3>();
    List<float> FleecaDoor_Heading = new List<float>();

    List<Vector3> FleecaVaultDoor_Locations = new List<Vector3>();
    List<float> FleecaVaultDoor_Heading = new List<float>();

    List<Vector3> FleecaVaultCash_Locations = new List<Vector3>();
    List<float> FleecaVaultCash_Heading = new List<float>();

    List<Vector3> FleecaCash_Locations = new List<Vector3>();
    List<float> FleecaCash_Heading = new List<float>();

    List<Vector3> FleecaVaultDoor_KeyPad_Locations = new List<Vector3>();

    List<Vector3> FleecaVaultDrill_Locations = new List<Vector3>();
    List<float> FleecaVaultDrill_Heading = new List<float>();

    List<Vector3> FleecaVaultBoxWall_Locations = new List<Vector3>();
    List<float> FleecaVaultBoxWall_Heading = new List<float>();

    List<Vector3> FleecaVaultBoxDoor_Locations = new List<Vector3>();
    List<float> FleecaVaultBoxDoor_Heading = new List<float>();

    int MinCash = 0;

    int MaxCash = 0;

    int WantedLevel = 4;

    int Passed_Index = -1;

    int time = 0;
    bool run = false;
    int Scale;
    int step = -1;

    string WhoDied;


    List<bool> BankAvalible = new List<bool>();

    bool BankRobbed1;
    bool BankRobbed2;
    bool BankRobbed3;
    bool BankRobbed4;
    bool BankRobbed5;
    bool BankRobbed6;

    bool ShowBlips;

    bool CashEnabled;
    bool DrillEnabled;

    public RobFleeca()
    {
        BankAvalible.Add(true);
        BankAvalible.Add(true);
        BankAvalible.Add(true);
        BankAvalible.Add(true);
        BankAvalible.Add(true);
        BankAvalible.Add(true);

        ////// bank 1 ocean
        Bank_Locations.Add(new Vector3(-2975.3f, 483.17f, 14.0f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(-2965.7f, 482.64f, 15.91f));

        Cam1_Locations.Add(new Vector3(-2962.176f, 486.5187f, 17.38165f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(-2960.813f, 476.3674f, 17.88708f));
        Cam3_Locations.Add(new Vector3(-2959.599f, 476.3453f, 17.88706f));
        Cam4_Locations.Add(new Vector3(-2958.923f, 478.9213f, 17.67277f));
        Cam5_Locations.Add(new Vector3(-2963.956f, 478.9629f, 17.34873f));  // -1340405475   prop_cctv_cam_07a


        Teller_Walk_Locations1.Add(new Vector3(-2960.813f, 477.5f, 17.88708f));
        Teller_Walk_Locations2.Add(new Vector3(-2958.3f, 477.7f, 17.88706f));

        Teller_Locations.Add(new Vector3(-2960.8f, 482.7f, 15.7f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(+90.0f);

        Hostage1_Locations.Add(new Vector3(-2964.2f, 486.7f, 14.2f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(+180.0f);

        Hostage2_Locations.Add(new Vector3(-2965.7f, 478.6f, 14.2f));
        Hostage2_Heading.Add(-90.0f);

        Hostage3_Locations.Add(new Vector3(-2963.0f, 481.6f, 14.7f));
        Hostage3_Heading.Add(-90.0f);

        FleecaDoor_Locations.Add(new Vector3(-2960.176f, 479.0105f, 15.97056f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(357.5421f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(-2956.3f, 481.6f, 15.8f));

        FleecaVaultDoor_Locations.Add(new Vector3(-2958.539f, 482.2706f, 15.83594f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(357.5421f);

        FleecaVaultCash_Locations.Add(new Vector3(-2954.120f, 484.1425f, 15.5256f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(-2954.871f, 484.1730f, 14.6762f));
        FleecaCash_Heading.Add(275f);

        FleecaVaultDrill_Locations.Add(new Vector3(-2953.2f, 484.715f, 14.7f));
        FleecaVaultDrill_Heading.Add(-92.5f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(-2952.2f, 484.3f, 15.88f));
        FleecaVaultBoxWall_Heading.Add(-92.5f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(-2952.37f, 484.81f, 17.039f));
        FleecaVaultBoxDoor_Heading.Add(-92.5f);

        ////// bank 2 near lifeinvader
        Bank_Locations.Add(new Vector3(-1220.5f, -317.5f, 36.4f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(-1213.96f, -327.28f, 36.8f));

        Cam1_Locations.Add(new Vector3(-1209.339f, -329.3076f, 39.4656f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(-1217.521f, -335.4675f, 39.97102f));
        Cam3_Locations.Add(new Vector3(-1216.972f, -336.5515f, 39.971f));
        Cam4_Locations.Add(new Vector3(-1214.37f, -335.864f, 39.75671f));
        Cam5_Locations.Add(new Vector3(-1216.798f, -331.4561f, 39.43268f));  // -1340405475   prop_cctv_cam_07a


        Teller_Walk_Locations1.Add(new Vector3(-1216.35f, -335.1f, 36.66f));
        Teller_Walk_Locations2.Add(new Vector3(-1215.4f, -337.2f, 36.6f));

        Teller_Locations.Add(new Vector3(-1212.2f, -332.5f, 37.8f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(26.8637f);

        Hostage1_Locations.Add(new Vector3(-1210.1f, -327.7f, 36.3f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(+115.0f);

        Hostage2_Locations.Add(new Vector3(-1217.9f, -330.1f, 36.3f));
        Hostage2_Heading.Add(205.0f);

        Hostage3_Locations.Add(new Vector3(-1214.1f, -331.3f, 36.8f));
        Hostage3_Heading.Add(165.0f);

        FleecaDoor_Locations.Add(new Vector3(-1214.905f, -334.7281f, 38.05551f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(296.8637f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(-1210.65f, -336.4f, 37.56f));

        FleecaVaultDoor_Locations.Add(new Vector3(-1211.261f, -334.5596f, 37.91989f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(296.8637f);

        FleecaVaultCash_Locations.Add(new Vector3(-1207.508f, -337.4475f, 37.6096f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(-1207.791f, -336.6265f, 36.7601f));
        FleecaCash_Heading.Add(205f);

        FleecaVaultDrill_Locations.Add(new Vector3(-1206.572f, -338.0f, 36.82f));
        FleecaVaultDrill_Heading.Add(-152.8f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(-1206.43f, -339.1f, 38.0f));
        FleecaVaultBoxWall_Heading.Add(-152.8f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(-1206.072f, -338.69f, 39.159f));
        FleecaVaultBoxDoor_Heading.Add(-152.8f);

        ////// bank 3  burton
        Bank_Locations.Add(new Vector3(-344.69f, -28.5f, 46.4f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(-349.67f, -46.4f, 49.0f));

        Cam1_Locations.Add(new Vector3(-347.6229f, -51.30733f, 50.72114f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(-357.7383f, -49.69931f, 51.22656f));
        Cam3_Locations.Add(new Vector3(-358.1367f, -50.84728f, 51.22655f));
        Cam4_Locations.Add(new Vector3(-355.8343f, -52.24216f, 51.01226f));
        Cam5_Locations.Add(new Vector3(-354.3498f, -47.43351f, 50.68822f));  // -1340405475   prop_cctv_cam_07a


        Teller_Walk_Locations1.Add(new Vector3(-356.8f, -50.15f, 48.2f));
        Teller_Walk_Locations2.Add(new Vector3(-357.6f, -52.4f, 48.2f));

        Teller_Locations.Add(new Vector3(-352.6f, -51.6f, 49.2f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(-25.0f);

        Hostage1_Locations.Add(new Vector3(-347.2f, -50.4f, 47.55f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(85.0f);

        Hostage2_Locations.Add(new Vector3(-356.6f, -46.6f, 47.55f));
        Hostage2_Heading.Add(-105.0f);

        Hostage3_Locations.Add(new Vector3(-352.1f, -49.2f, 48.1f));
        Hostage3_Heading.Add(-195.0f);

        FleecaDoor_Locations.Add(new Vector3(-355.3892f, -51.06769f, 49.31105f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(250.5898f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(-353.7f, -55.3f, 49.0f));

        FleecaVaultDoor_Locations.Add(new Vector3(-352.7365f, -53.57248f, 49.17543f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(250.8598f);

        FleecaVaultCash_Locations.Add(new Vector3(-352.2960f, -58.2996f, 48.8651f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(-351.9788f, -57.6870f, 48.0156f));
        FleecaCash_Heading.Add(185f);

        FleecaVaultDrill_Locations.Add(new Vector3(-351.918f, -59.38f, 48.05f));
        FleecaVaultDrill_Heading.Add(160.75f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(-352.6f, -60.2f, 49.225f));
        FleecaVaultBoxWall_Heading.Add(160.75f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(-352.06f, -60.18f, 50.384f));
        FleecaVaultBoxDoor_Heading.Add(160.75f);

        ////// bank 4  alta
        Bank_Locations.Add(new Vector3(318.1f, -266.1f, 52.6f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(315.3f, -276.1f, 54.0f));

        Cam1_Locations.Add(new Vector3(317.5102f, -280.554f, 55.84935f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(307.4242f, -278.7708f, 56.35477f));
        Cam3_Locations.Add(new Vector3(307.0059f, -279.9117f, 56.35476f));
        Cam4_Locations.Add(new Vector3(309.2838f, -281.3463f, 56.14047f));
        Cam5_Locations.Add(new Vector3(310.81643f, -276.5641f, 55.81643f));  // -1340405475   prop_cctv_cam_07a

        Teller_Walk_Locations1.Add(new Vector3(308.5f, -278.85f, 53.3f));
        Teller_Walk_Locations2.Add(new Vector3(307.7f, -281.3f, 53.3f));

        Teller_Locations.Add(new Vector3(313.6f, -280.0f, 54.2f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(-25.0f);

        Hostage1_Locations.Add(new Vector3(318.3f, -278.65f, 52.65f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(70.0f);

        Hostage2_Locations.Add(new Vector3(311.5f, -274.9f, 52.65f));
        Hostage2_Heading.Add(-195.0f);

        Hostage3_Locations.Add(new Vector3(314.4f, -278.6f, 53.2f));
        Hostage3_Heading.Add(-195.0f);

        FleecaDoor_Locations.Add(new Vector3(309.7491f, -280.1797f, 54.43926f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(249.866f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(311.35f, -284.45f, 54.0f));

        FleecaVaultDoor_Locations.Add(new Vector3(312.358f, -282.73018f, 54.30365f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(249.866f);

        FleecaVaultCash_Locations.Add(new Vector3(312.7440f, -287.4624f, 53.9933f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(313.0498f, -286.8682f, 53.1439f));
        FleecaCash_Heading.Add(175f);

        FleecaVaultDrill_Locations.Add(new Vector3(313.078f, -288.54f, 53.2f));
        FleecaVaultDrill_Heading.Add(160.0f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(312.39f, -289.35f, 54.38f));
        FleecaVaultBoxWall_Heading.Add(160.0f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(312.928f, -289.339f, 55.539f));
        FleecaVaultBoxDoor_Heading.Add(160.0f);

        ////// bank 5  grand senora
        Bank_Locations.Add(new Vector3(1175.4f, 2690.9f, 36.6f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(1175.2f, 2703.75f, 38.0f));

        Cam1_Locations.Add(new Vector3(1171.454f, 2707.45f, 39.77259f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(1181.537f, 2708.842f, 40.27802f));
        Cam3_Locations.Add(new Vector3(1181.537f, 2710.057f, 40.278f));
        Cam4_Locations.Add(new Vector3(1178.905f, 2710.62f, 40.06371f));
        Cam5_Locations.Add(new Vector3(1179.079f, 2705.591f, 39.73968f));  // -1340405475   prop_cctv_cam_07a

        Teller_Walk_Locations1.Add(new Vector3(1180.4f, 2708.5f, 38.0f));
        Teller_Walk_Locations2.Add(new Vector3(1180.4f, 2711.2f, 38.0f));

        Teller_Locations.Add(new Vector3(1175.0f, 2709.0f, 38.0f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(180.0f);

        Hostage1_Locations.Add(new Vector3(1171.4f, 2705.1f, 36.59f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(-90.0f);

        Hostage2_Locations.Add(new Vector3(1179.4f, 2703.8f, 36.59f));
        Hostage2_Heading.Add(.0f);

        Hostage3_Locations.Add(new Vector3(1175.0f, 2706.5f, 37.145f));
        Hostage3_Heading.Add(.0f);

        FleecaDoor_Locations.Add(new Vector3(1178.87f, 2709.365f, 38.36251f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(90.0f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(1175.9f, 2712.8f, 38.0f));

        FleecaVaultDoor_Locations.Add(new Vector3(1175.542f, 2710.861f, 38.22689f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(90.0f);

        FleecaVaultCash_Locations.Add(new Vector3(1173.5382f, 2715.1797f, 37.9166f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(1173.4769f, 2714.4868f, 37.0671f));
        FleecaCash_Heading.Add(18f);

        FleecaVaultDrill_Locations.Add(new Vector3(1172.855f, 2716.1f, 37.14f));
        FleecaVaultDrill_Heading.Add(0.0f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(1173.23f, 2717.1f, 38.32f));
        FleecaVaultBoxWall_Heading.Add(0.0f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(1172.725f, 2716.91f, 39.48f));
        FleecaVaultBoxDoor_Heading.Add(0.0f);


        ////// bank 6 Vespucci_Boulevard
        Bank_Locations.Add(new Vector3(153.2f, -1029.0f, 28.0f));  // green marker

        EnterExitBank_Locations.Add(new Vector3(151.3f, -1036.6f, 29.0f));

        Cam1_Locations.Add(new Vector3(153.1795f, -1042.05264f, 31.05264f));   // 168901740 prop_cctv_cam_06a coords for cams
        Cam2_Locations.Add(new Vector3(143.0941f, -1040.403f, 31.55806f));
        Cam3_Locations.Add(new Vector3(142.5754f, -1041.543f, 31.55805f));
        Cam4_Locations.Add(new Vector3(144.9529f, -1042.979f, 31.34375f));
        Cam5_Locations.Add(new Vector3(146.5221f, -1038.197f, 31.01972f));  // -1340405475   prop_cctv_cam_07a

        Teller_Walk_Locations1.Add(new Vector3(144.7f, -1040.6f, 29.0f));
        Teller_Walk_Locations2.Add(new Vector3(143.3f, -1043.2f, 29.0f));

        Teller_Locations.Add(new Vector3(149.7f, -1042.4f, 29.0f));  // teller  a_m_y_business_01   a_m_y_busicas_01
        Teller_Heading.Add(0.0f);

        Hostage1_Locations.Add(new Vector3(154.0f, -1040.3f, 27.85f));  // left seat Hostage1   a_m_y_stbla_01  a_m_y_stbla_02  a_f_y_tourist_02  a_f_y_tourist_01  a_m_m_stlat_02    a_m_y_genstreet_01  a_f_y_business_03
        Hostage1_Heading.Add(69.8462f);

        Hostage2_Locations.Add(new Vector3(146.7f, -1036.5f, 27.85f));
        Hostage2_Heading.Add(159.8462f);

        Hostage3_Locations.Add(new Vector3(150.0f, -1040.6f, 28.4f));
        Hostage3_Heading.Add(159.8462f);

        FleecaDoor_Locations.Add(new Vector3(145.4186f, -1041.813f, 29.64255f));       // -131754413 v_ilev_gb_teldr heading 145.0371 fleeca door
        FleecaDoor_Heading.Add(249.8462f);

        FleecaVaultDoor_KeyPad_Locations.Add(new Vector3(146.9f, -1046.0f, 29.0f));

        FleecaVaultDoor_Locations.Add(new Vector3(148.0266f, -1044.364f, 29.50693f));  // 2121050683 v_ilev_gb_vauldr  heading 357.5421 valt door 

        FleecaVaultDoor_Heading.Add(249.8462f);

        FleecaVaultCash_Locations.Add(new Vector3(148.4134f, -1049.139f, 29.1966f));
        FleecaVaultCash_Heading.Add(52.78f);

        FleecaCash_Locations.Add(new Vector3(148.7058f, -1048.553f, 28.3471f));
        FleecaCash_Heading.Add(175f);

        FleecaVaultDrill_Locations.Add(new Vector3(148.748f, -1050.2f, 28.365f));
        FleecaVaultDrill_Heading.Add(159.8462f);

        FleecaVaultBoxWall_Locations.Add(new Vector3(148.05f, -1051.0f, 29.55f));
        FleecaVaultBoxWall_Heading.Add(159.8462f);

        FleecaVaultBoxDoor_Locations.Add(new Vector3(148.59f, -1051.0f, 30.70f));
        FleecaVaultBoxDoor_Heading.Add(159.8462f);

        /////////////////////

        KeysData = ScriptSettings.Load("scripts//RobFleeca.ini");

        MinCash = KeysData.GetValue<int>("settings", "MinCash", 25000);

        MaxCash = KeysData.GetValue<int>("settings", "MaxCash", 250000);

        WantedLevel = KeysData.GetValue<int>("settings", "WantedLevel", 4);

        ShowBlips = KeysData.GetValue<bool>("settings", "ShowBlips", true);

        BankRobbed1 = KeysData.GetValue<bool>("settings", "BankRobbed1", false);

        BankRobbed2 = KeysData.GetValue<bool>("settings", "BankRobbed2", false);

        BankRobbed3 = KeysData.GetValue<bool>("settings", "BankRobbed3", false);

        BankRobbed4 = KeysData.GetValue<bool>("settings", "BankRobbed4", false);

        BankRobbed5 = KeysData.GetValue<bool>("settings", "BankRobbed5", false);

        BankRobbed6 = KeysData.GetValue<bool>("settings", "BankRobbed6", false);

        CashEnabled = KeysData.GetValue<bool>("settings", "CashEnabled", true);

        DrillEnabled = KeysData.GetValue<bool>("settings", "DrillEnabled", true);


        Blip[] blips = World.GetActiveBlips();

        if (blips.Count() > 0)
        {
            for (int i2 = 0; i2 < blips.Count(); i2++)
            {
                for (int i = 0; i < Bank_Locations.Count; i++)
                {
                    if (blips[i2].Position == Bank_Locations[i])
                    {
                        blips[i2].Remove();
                    }
                }
            }
        }

        if (ShowBlips)
        {
            for (int i = 0; i < Bank_Locations.Count; i++)
            {
                Blip b = World.CreateBlip(Bank_Locations[i]);
                b.Sprite = BlipSprite.DollarBill;
                b.Color = BlipColor.Green;
                b.IsShortRange = true;
                b.Name = "Fleeca Bank";
                Function.Call(Hash.SET_FAKE_WANTED_LEVEL, 0);
            }
        }

        //_SET_NOTIFICATION_MESSAGE(char * picName1, char * picName2, BOOL flash, int iconType, BOOL _nonExistent, char * sender, char * subject)
        //Function.Call(Hash._SET_NOTIFICATION_MESSAGE("CHAR_LESTER", "CHAR_LESTER", false, int iconType, BOOL _nonExistent, char * sender, char * subject));
        UI.Notify("RobFleeca by Aimless, v2 by danistheman262", false);

        Tick += OnTick;
        KeyUp += OnKeyUp;

    }


    public void OnTick(object o, EventArgs e)
    {

        //Prop[] tmppop = World.GetNearbyProps(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1 + Game.Player.Character.UpVector * .5f, 2.0f);    // "prop_facgate_04_l","prop_facgate_04_r",

        //if (tmppop.Count() > 0)
        //{
        //    World.DrawMarker(MarkerType.UpsideDownCone, tmppop[0].Position, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 180.0f, 0.0f), new Vector3(1.0f, 1.0f, 2.0f), Color.Red, true, true, 2, false, "", "", false);

        //    //UI.ShowSubtitle(tmppop[0].Model.Hash.ToString(), 10);
        //    UI.ShowSubtitle(tmppop[0].Model.Hash.ToString() + "   " + tmppop[0].Position.ToString() + "   " + tmppop[0].Heading.ToString() + "   " + tmppop[0].Health.ToString() + "  " + Function.Call<int>(Hash.GET_ENTITY_LOD_DIST, tmppop[0].Handle).ToString(), 10);

        //    //Function.Call<string>(Hash._0x717E4D1F2048376D, tmppop[0].Handle);
        //}

        //Ped tmpped = World.GetClosestPed(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3, 1.0f);    // "prop_facgate_04_l","prop_facgate_04_r",

        //if (tmpped != null)
        //{
        //    World.DrawMarker(MarkerType.UpsideDownCone, tmpped.Position, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 180.0f, 0.0f), new Vector3(1.0f, 1.0f, 2.0f), Color.Red, true, true, 2, false, "", "", false);

        //    //UI.ShowSubtitle(tmppop[0].Model.Hash.ToString(), 10);
        //    UI.ShowSubtitle(tmpped.Model.Hash.ToString() + "   " + tmpped.Position.ToString() + "   " + tmpped.Heading.ToString() + "   " + tmpped.Health.ToString() + "  " + Function.Call<int>(Hash.GET_ENTITY_LOD_DIST, tmpped.Handle).ToString(), 10);

        //    //Function.Call<string>(Hash._0x717E4D1F2048376D, tmppop[0].Handle);
        //}
        //Game.Player.WantedLevel = 0;

        if (Function.Call<bool>(Hash.IS_CONTROL_JUST_PRESSED, 2, 175)) // start if dpad left
        {
            if (Game.Player.WantedLevel < 1 && Robs.Count < 1 && Function.Call<int>(Hash.GET_CLOCK_HOURS) >= 7 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 18)
            {
                for (int i = 0; i < Bank_Locations.Count; i++)
                {
                    if (Bank_Locations[i].DistanceTo(Game.Player.Character.Position) < 12.0f && BankAvalible[i] == true)
                    {
                        Rob rob = new Rob(i);
                        Robs.Add(rob);
                    }
                }
            }
        }

        if (Robs.Count > 0)
        {

            switch (Robs[0].Objective_Index)
            {
                case 0:
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, Robs[0].Checkpoint_Sound, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);

                        Ped p = World.GetClosestPed(Teller_Locations[Robs[0].List_Index], 1.0f);

                        if (p != null && p.IsDead != true)
                        {
                            Robs[0].Teller = p;
                            p.IsPersistent = true;
                        }
                        else
                        {
                            int modelSelect = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 100);

                            string mdl = "a_f_y_business_03";

                            if (modelSelect >= 50)
                            {
                                mdl = "a_f_y_business_02";
                            }
                            if (modelSelect > 50)
                            {
                                mdl = "a_m_y_business_02";
                            }

                            var model = new Model(mdl);
                            model.Request(250);

                            if (model.IsInCdImage && model.IsValid)
                            {
                                while (!model.IsLoaded) Script.Wait(50);
                                Ped ped = World.CreatePed(model, Teller_Locations[Robs[0].List_Index], Teller_Heading[Robs[0].List_Index]);
                                Robs[0].Teller = ped;
                                ped.IsPersistent = true;
                                ped.RandomizeOutfit();
                                Function.Call(Hash.TASK_START_SCENARIO_AT_POSITION, ped, "WORLD_HUMAN_CLIPBOARD", Teller_Locations[Robs[0].List_Index].X, Teller_Locations[Robs[0].List_Index].Y, Teller_Locations[Robs[0].List_Index].Z, Teller_Heading[Robs[0].List_Index], -1, 0, 1);
                            }

                            model.MarkAsNoLongerNeeded();

                        }

                        Ped Hostage1 = World.GetClosestPed(Hostage1_Locations[Robs[0].List_Index], 1.0f);

                        if (Hostage1 != null)
                        {
                            Robs[0].Hostage1 = Hostage1;
                            Robs[0].Hostage1.CanBeTargetted = false;
                            Hostage1.IsPersistent = true;
                        }
                        else
                        {
                            int modelSelect = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 7);

                            string mdl = "a_m_y_stbla_01";

                            switch (modelSelect)
                            {
                                case 1:
                                    {
                                        mdl = "a_m_y_stbla_01";
                                    }
                                    break;
                                case 2:
                                    {
                                        mdl = "a_m_y_stbla_02";
                                    }
                                    break;
                                case 3:
                                    {
                                        mdl = "a_f_y_tourist_01";
                                    }
                                    break;
                                case 4:
                                    {
                                        mdl = "a_f_y_tourist_02";
                                    }
                                    break;
                                case 5:
                                    {
                                        mdl = "a_m_m_stlat_02";
                                    }
                                    break;
                                case 6:
                                    {
                                        mdl = "a_m_y_genstreet_01";
                                    }
                                    break;
                                case 7:
                                    {
                                        mdl = "a_f_y_business_03";
                                    }
                                    break;
                            }

                            var model = new Model(mdl);
                            model.Request(250);

                            if (model.IsInCdImage && model.IsValid)
                            {
                                while (!model.IsLoaded) Script.Wait(50);
                                Ped ped = World.CreatePed(model, Hostage1_Locations[Robs[0].List_Index], Hostage1_Heading[Robs[0].List_Index]);

                                Function.Call(Hash.TASK_START_SCENARIO_AT_POSITION, ped, "PROP_HUMAN_SEAT_BENCH", ped.Position.X, ped.Position.Y, ped.Position.Z, Hostage1_Heading[Robs[0].List_Index], -1, 1, 0);

                                Robs[0].Hostage1 = ped;
                                Robs[0].Hostage1.CanBeTargetted = false;

                                ped.IsPersistent = true;
                                ped.RandomizeOutfit();
                            }

                            model.MarkAsNoLongerNeeded();

                        }

                        Ped Hostage2 = World.GetClosestPed(Hostage2_Locations[Robs[0].List_Index], 1.0f);

                        if (Hostage2 != null)
                        {
                            Robs[0].Hostage2 = Hostage2;
                            Robs[0].Hostage2.CanBeTargetted = false;

                            Hostage2.IsPersistent = true;
                        }
                        else
                        {
                            int modelSelect = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 7);

                            string mdl = "a_m_y_stbla_01";

                            switch (modelSelect)
                            {
                                case 1:
                                    {
                                        mdl = "a_m_y_stbla_01";
                                    }
                                    break;
                                case 2:
                                    {
                                        mdl = "a_m_y_stbla_02";
                                    }
                                    break;
                                case 3:
                                    {
                                        mdl = "a_f_y_tourist_01";
                                    }
                                    break;
                                case 4:
                                    {
                                        mdl = "a_f_y_tourist_02";
                                    }
                                    break;
                                case 5:
                                    {
                                        mdl = "a_m_m_stlat_02";
                                    }
                                    break;
                                case 6:
                                    {
                                        mdl = "a_m_y_genstreet_01";
                                    }
                                    break;
                                case 7:
                                    {
                                        mdl = "a_f_y_business_03";
                                    }
                                    break;
                            }

                            var model = new Model(mdl);
                            model.Request(250);

                            if (model.IsInCdImage && model.IsValid)
                            {
                                while (!model.IsLoaded) Script.Wait(50);
                                Ped ped = World.CreatePed(model, Hostage2_Locations[Robs[0].List_Index], Hostage2_Heading[Robs[0].List_Index]);

                                Function.Call(Hash.TASK_START_SCENARIO_AT_POSITION, ped, "PROP_HUMAN_SEAT_BENCH", ped.Position.X, ped.Position.Y, ped.Position.Z, Hostage2_Heading[Robs[0].List_Index], -1, 1, 0);

                                Robs[0].Hostage2 = ped;

                                Robs[0].Hostage2.CanBeTargetted = false;

                                ped.IsPersistent = true;
                                ped.RandomizeOutfit();
                            }

                            model.MarkAsNoLongerNeeded();

                        }

                        Ped Hostage3 = World.GetClosestPed(Hostage3_Locations[Robs[0].List_Index], 1.0f);

                        if (Hostage3 != null)
                        {
                            Robs[0].Hostage3 = Hostage3;
                            Robs[0].Hostage3.CanBeTargetted = false;

                            Hostage3.IsPersistent = true;
                        }
                        else
                        {
                            int modelSelect = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 1, 7);

                            string mdl = "a_m_y_stbla_01";

                            switch (modelSelect)
                            {
                                case 1:
                                    {
                                        mdl = "a_m_y_stbla_01";
                                    }
                                    break;
                                case 2:
                                    {
                                        mdl = "a_m_y_stbla_02";
                                    }
                                    break;
                                case 3:
                                    {
                                        mdl = "a_f_y_tourist_01";
                                    }
                                    break;
                                case 4:
                                    {
                                        mdl = "a_f_y_tourist_02";
                                    }
                                    break;
                                case 5:
                                    {
                                        mdl = "a_m_m_stlat_02";
                                    }
                                    break;
                                case 6:
                                    {
                                        mdl = "a_m_y_genstreet_01";
                                    }
                                    break;
                                case 7:
                                    {
                                        mdl = "a_f_y_business_03";
                                    }
                                    break;
                            }

                            var model = new Model(mdl);
                            model.Request(250);

                            if (model.IsInCdImage && model.IsValid)
                            {
                                while (!model.IsLoaded) Script.Wait(50);
                                Ped ped = World.CreatePed(model, Hostage3_Locations[Robs[0].List_Index], Hostage3_Heading[Robs[0].List_Index]);

                                Function.Call(Hash.TASK_START_SCENARIO_AT_POSITION, ped, "PROP_HUMAN_STAND_IMPATIENT", ped.Position.X, ped.Position.Y, ped.Position.Z, Hostage3_Heading[Robs[0].List_Index], -1, 0, 0);

                                Robs[0].Hostage3 = ped;
                                Robs[0].Hostage3.CanBeTargetted = false;

                                ped.IsPersistent = true;
                                ped.RandomizeOutfit();
                            }

                            model.MarkAsNoLongerNeeded();

                        }
                        Prop[] prop = World.GetNearbyProps(Cam1_Locations[Robs[0].List_Index], 0.5f);
                        if (prop.Count() > 0)
                        {
                            for (int i = 0; i < prop.Count(); i++)
                            {
                                if (prop[i].Model == 168901740 || prop[i].Model == -1340405475 || prop[i].Model == -1007354661 || prop[i].Model == -1842407088)
                                {
                                    Robs[0].Cam1 = prop[i];
                                    Robs[0].Cam1.AddBlip();

                                    Robs[0].Cam1.CurrentBlip.Sprite = BlipSprite.Crosshair2;
                                    Robs[0].Cam1.CurrentBlip.Color = BlipColor.Red;
                                    Robs[0].Cam1.CurrentBlip.IsFlashing = false;
                                    Robs[0].Cam1.CurrentBlip.Scale = .65f;
                                }
                            }
                        }

                        Prop[] prop2 = World.GetNearbyProps(Cam2_Locations[Robs[0].List_Index], 0.5f);
                        if (prop2.Count() > 0)
                        {
                            for (int i = 0; i < prop2.Count(); i++)
                            {
                                if (prop2[i].Model == 168901740 || prop2[i].Model == -1340405475 || prop[i].Model == -1007354661 || prop[i].Model == -1842407088)
                                {
                                    Robs[0].Cam2 = prop2[i];
                                    Robs[0].Cam2.AddBlip();
                                    Robs[0].Cam2.CurrentBlip.Sprite = BlipSprite.Crosshair2;
                                    Robs[0].Cam2.CurrentBlip.Color = BlipColor.Red;
                                    Robs[0].Cam2.CurrentBlip.IsFlashing = false;
                                    Robs[0].Cam2.CurrentBlip.Scale = .65f;
                                }
                            }
                        }

                        Prop[] prop3 = World.GetNearbyProps(Cam3_Locations[Robs[0].List_Index], 0.5f);
                        if (prop3.Count() > 0)
                        {
                            for (int i = 0; i < prop3.Count(); i++)
                            {
                                if (prop3[i].Model == 168901740 || prop3[i].Model == -1340405475 || prop[i].Model == -1007354661 || prop[i].Model == -1842407088)
                                {
                                    Robs[0].Cam3 = prop3[i];
                                    Robs[0].Cam3.AddBlip();
                                    Robs[0].Cam3.CurrentBlip.Sprite = BlipSprite.Crosshair2;
                                    Robs[0].Cam3.CurrentBlip.Color = BlipColor.Red;
                                    Robs[0].Cam3.CurrentBlip.IsFlashing = false;
                                    Robs[0].Cam3.CurrentBlip.Scale = .65f;
                                }
                            }
                        }

                        Prop[] prop4 = World.GetNearbyProps(Cam4_Locations[Robs[0].List_Index], 0.5f);
                        if (prop4.Count() > 0)
                        {
                            for (int i = 0; i < prop4.Count(); i++)
                            {
                                if (prop4[i].Model == 168901740 || prop4[i].Model == -1340405475 || prop[i].Model == -1007354661 || prop[i].Model == -1842407088)
                                {
                                    Robs[0].Cam4 = prop4[i];
                                    Robs[0].Cam4.AddBlip();
                                    Robs[0].Cam4.CurrentBlip.Sprite = BlipSprite.Crosshair2;
                                    Robs[0].Cam4.CurrentBlip.Color = BlipColor.Red;
                                    Robs[0].Cam4.CurrentBlip.IsFlashing = false;
                                    Robs[0].Cam4.CurrentBlip.Scale = .65f;
                                }
                            }
                        }

                        Prop[] prop5 = World.GetNearbyProps(Cam5_Locations[Robs[0].List_Index], 0.5f);
                        if (prop5.Count() > 0)
                        {
                            for (int i = 0; i < prop5.Count(); i++)
                            {
                                if (prop5[i].Model == 168901740 || prop5[i].Model == -1340405475 || prop[i].Model == -1007354661 || prop[i].Model == -1842407088)
                                {
                                    Robs[0].Cam5 = prop5[i];
                                    Robs[0].Cam5.AddBlip();
                                    Robs[0].Cam5.CurrentBlip.Sprite = BlipSprite.Crosshair2;
                                    Robs[0].Cam5.CurrentBlip.Color = BlipColor.Red;
                                    Robs[0].Cam5.CurrentBlip.IsFlashing = false;
                                    Robs[0].Cam5.CurrentBlip.Scale = .65f;
                                }
                            }
                        }

                        Prop[] prop6 = World.GetNearbyProps(FleecaDoor_Locations[Robs[0].List_Index], 0.5f);
                        if (prop6.Count() > 0)
                        {
                            for (int i = 0; i < prop6.Count(); i++)
                            {
                                if (prop6[i].Model == -131754413)
                                {
                                    prop6[i].Heading = FleecaDoor_Heading[Robs[0].List_Index];
                                }
                            }
                        }

                        Prop[] prop8 = World.GetNearbyProps(FleecaVaultDoor_Locations[Robs[0].List_Index], 1.5f);
                        if (prop8.Count() > 0)
                        {
                            for (int i = 0; i < prop8.Count(); i++)
                            {
                                if (prop8[i].Model == 2121050683 || prop8[i].Model == -63539571)
                                {
                                    prop8[i].Delete();

                                }
                            }
                        }
                        var modelv = new Model("hei_prop_heist_sec_door"); // 
                        modelv.Request(250);
                        if (modelv.IsInCdImage && modelv.IsValid)
                        {
                            while (!modelv.IsLoaded) Script.Wait(50);
                            Prop door = World.CreateProp(modelv, new Vector3(FleecaVaultDoor_Locations[Robs[0].List_Index].X, FleecaVaultDoor_Locations[Robs[0].List_Index].Y, FleecaVaultDoor_Locations[Robs[0].List_Index].Z - 1.2f), new Vector3(0.0f, 0.0f, FleecaVaultDoor_Heading[Robs[0].List_Index]), true, false);
                            door.FreezePosition = true;
                        }

                        modelv.MarkAsNoLongerNeeded();

                        Prop[] prop9 = World.GetNearbyProps(FleecaVaultBoxWall_Locations[Robs[0].List_Index], 1.0f);
                        if (prop9.Count() > 0)
                        {
                            for (int i = 0; i < prop9.Count(); i++)
                            {
                                if (prop9[i].Model == 152330975)
                                {
                                    prop9[i].Delete();
                                }
                            }
                        }
                        var modelWall = new Model("hei_prop_heist_safedeposit"); // 
                        modelWall.Request(250);
                        if (modelWall.IsInCdImage && modelWall.IsValid)
                        {
                            while (!modelWall.IsLoaded) Script.Wait(50);
                            Prop wall = World.CreateProp(modelWall, new Vector3(FleecaVaultBoxWall_Locations[Robs[0].List_Index].X, FleecaVaultBoxWall_Locations[Robs[0].List_Index].Y, FleecaVaultBoxWall_Locations[Robs[0].List_Index].Z - 1.2f), new Vector3(0.0f, 0.0f, FleecaVaultBoxWall_Heading[Robs[0].List_Index]), true, false);
                            Robs[0].BoxWall = wall;
                            wall.FreezePosition = true;
                        }

                        modelWall.MarkAsNoLongerNeeded();

                        Prop[] prop10 = World.GetNearbyProps(FleecaVaultBoxDoor_Locations[Robs[0].List_Index], 1.5f);
                        if (prop10.Count() > 0)
                        {
                            for (int i = 0; i < prop10.Count(); i++)
                            {
                                if (prop10[i].Model == -812777085)
                                {
                                    prop10[i].Delete();
                                }
                            }
                        }
                        var modelDoor = new Model("hei_prop_heist_safedepdoor"); // 
                        modelDoor.Request(250);
                        if (modelDoor.IsInCdImage && modelDoor.IsValid)
                        {
                            while (!modelDoor.IsLoaded) Script.Wait(50);
                            Prop door = World.CreateProp(modelDoor, new Vector3(FleecaVaultBoxDoor_Locations[Robs[0].List_Index].X, FleecaVaultBoxDoor_Locations[Robs[0].List_Index].Y, FleecaVaultBoxDoor_Locations[Robs[0].List_Index].Z - 1.2f), new Vector3(0.0f, 0.0f, FleecaVaultBoxDoor_Heading[Robs[0].List_Index]), true, false);
                            Robs[0].BoxDoor = door;
                            door.FreezePosition = true;
                        }

                        modelDoor.MarkAsNoLongerNeeded();

                        Prop[] prop11 = World.GetNearbyProps(FleecaVaultBoxDoor_Locations[Robs[0].List_Index], 1.5f);
                        if (prop11.Count() > 0)
                        {
                            for (int i = 0; i < prop11.Count(); i++)
                            {
                                if (prop11[i].Model == 264881854)
                                {
                                    prop11[i].Delete();
                                }
                            }
                        }
                        var modelenvelope = new Model("p_cash_envelope_01_s"); // 
                        modelenvelope.Request(250);
                        if (modelenvelope.IsInCdImage && modelenvelope.IsValid)
                        {
                            while (!modelenvelope.IsLoaded) Script.Wait(50);
                            Prop envelope = World.CreateProp(modelenvelope, Robs[0].BoxDoor.Position + Robs[0].BoxDoor.ForwardVector * +.12f + Robs[0].BoxDoor.RightVector * +.15f + Robs[0].BoxDoor.UpVector * -.1f, new Vector3(90.0f, 45.0f, Robs[0].BoxDoor.Heading), true, false);
                            Robs[0].CashEnvelope = envelope;
                            envelope.FreezePosition = true;
                        }

                        modelenvelope.MarkAsNoLongerNeeded();

                        UI.ShowSubtitle("~r~Shoot ~w~the ~y~Cameras.", 30000);

                        Robs[0].Objective_Index = 1;


                    }
                    break;
                case 1:
                    {
                        UI.ShowSubtitle("~r~Shoot ~w~the ~y~Cameras.", 1000);

                        if (Robs[0].Cam1 != null && Robs[0].Cam1.Health < 900)
                        {
                            Robs[0].Cam1.CurrentBlip.Remove();
                            Robs[0].Cam1.MarkAsNoLongerNeeded();
                        }

                        if (Robs[0].Cam2 != null && Robs[0].Cam2.Health < 900)
                        {
                            Robs[0].Cam2.CurrentBlip.Remove();
                            Robs[0].Cam2.MarkAsNoLongerNeeded();
                        }
                        if (Robs[0].Cam3 != null && Robs[0].Cam3.Health < 900)
                        {
                            Robs[0].Cam3.CurrentBlip.Remove();
                            Robs[0].Cam3.MarkAsNoLongerNeeded();
                        }
                        if (Robs[0].Cam4 != null && Robs[0].Cam4.Health < 900)
                        {
                            Robs[0].Cam4.CurrentBlip.Remove();
                            Robs[0].Cam4.MarkAsNoLongerNeeded();
                        }
                        if (Robs[0].Cam5 != null && Robs[0].Cam5.Health < 900)
                        {
                            Robs[0].Cam5.CurrentBlip.Remove();
                            Robs[0].Cam5.MarkAsNoLongerNeeded();
                        }

                        if (Robs[0].Cam1 != null && Robs[0].Cam2 != null && Robs[0].Cam3 != null && Robs[0].Cam4 != null && Robs[0].Cam5 != null &&
                            Robs[0].Cam1.Health < 900 && Robs[0].Cam2.Health < 900 && Robs[0].Cam3.Health < 900 && Robs[0].Cam4.Health < 900 && Robs[0].Cam5.Health < 900)
                        {
                            Robs[0].Objective_Index = 2;
                            Robs[0].Teller.AddBlip();
                            Robs[0].Teller.CurrentBlip.Sprite = BlipSprite.VIP;
                            Robs[0].Teller.CurrentBlip.Color = BlipColor.Blue;
                            Robs[0].Teller.CurrentBlip.Scale = .75f;
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, Robs[0].Checkpoint_Sound, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);
                        }

                        if (Robs[0].Cam1 == null || Robs[0].Cam2 == null && Robs[0].Cam3 == null || Robs[0].Cam4 == null | Robs[0].Cam5 == null ||
                      Robs[0].Cam1.Health < 900 || Robs[0].Cam2.Health < 900 || Robs[0].Cam3.Health < 900 || Robs[0].Cam4.Health < 900 || Robs[0].Cam5.Health < 900)
                        {

                            Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);

                            //Game.Player.WantedLevel = 0;

                            if (!Robs[0].TellerCower)
                            {
                                Robs[0].TellerCower = true;

                                Function.Call(Hash.SET_PED_COWER_HASH, Robs[0].Teller, "CODE_HUMAN_STAND_COWER");

                                Robs[0].Teller.Task.Cower(-1);
                            }

                            if (!Robs[0].Hostage3Cower)
                            {
                                Robs[0].Hostage3Cower = true;

                                Robs[0].Hostage3.Task.ClearAllImmediately();

                                Function.Call(Hash.SET_PED_COWER_HASH, Robs[0].Hostage3, "CODE_HUMAN_STAND_COWER");

                                Robs[0].Hostage3.Task.Cower(-1);
                            }
                        }

                        if (Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING_AT_ENTITY, Game.Player, Robs[0].Teller) && !Robs[0].TellerCower)
                        {
                            Robs[0].TellerCower = true;

                            Function.Call(Hash.SET_PED_COWER_HASH, Robs[0].Teller, "CODE_HUMAN_STAND_COWER");

                            Robs[0].Teller.Task.Cower(-1);
                        }

                        if (Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING_AT_ENTITY, Game.Player, Robs[0].Hostage3) && !Robs[0].Hostage3Cower)
                        {
                            Robs[0].Hostage3Cower = true;

                            Function.Call(Hash.SET_PED_COWER_HASH, Robs[0].Hostage3, "CODE_HUMAN_STAND_COWER");

                            Robs[0].Hostage3.Task.Cower(-1);
                        }


                    }
                    break;
                case 2:
                    {
                        Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, Robs[0].Teller, true);

                        if (Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING_AT_ENTITY, Game.Player, Robs[0].Teller))
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, Robs[0].Checkpoint_Sound, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);

                            Robs[0].Teller.Task.ClearAllImmediately();

                            Robs[0].Objective_Index = 3;
                        }

                        UI.ShowSubtitle("~r~Intimidate ~w~the ~b~teller.", 30000);

                        Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);



                        Game.Player.WantedLevel = 0;

                    }
                    break;
                case 3:
                    {
                        Function.Call(Hash.SET_PED_COWER_HASH, Robs[0].Teller, "CODE_HUMAN_COWER");

                        unsafe
                        {
                            int seq = 0;
                            GTA.Native.Function.Call(Hash.OPEN_SEQUENCE_TASK, &seq);

                            Function.Call(Hash.TASK_PLAY_ANIM, 0, "ped", "handsup", 4.0f, .0f, -1, 49, .0f, 0, 0, 0);

                            Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, 0, Teller_Walk_Locations1[Robs[0].List_Index].X, Teller_Walk_Locations1[Robs[0].List_Index].Y, Teller_Walk_Locations1[Robs[0].List_Index].Z, 1.0f, -1, Teller_Heading[Robs[0].List_Index] + 100.0f, -.1f);
                            Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, 0, Teller_Walk_Locations2[Robs[0].List_Index].X, Teller_Walk_Locations2[Robs[0].List_Index].Y, Teller_Walk_Locations2[Robs[0].List_Index].Z, 1.0f, -1, Teller_Heading[Robs[0].List_Index] - 100.0f, -.1f);
                            Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, 0, FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].X, FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].Y, FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].Z, 1.2f, -1, Teller_Heading[Robs[0].List_Index] - 90.0f, -.1f);

                            GTA.Native.Function.Call(Hash.SET_SEQUENCE_TO_REPEAT, seq, false);
                            GTA.Native.Function.Call(Hash.CLOSE_SEQUENCE_TASK, seq);
                            GTA.Native.Function.Call(Hash.TASK_PERFORM_SEQUENCE, Robs[0].Teller, seq);
                            GTA.Native.Function.Call(Hash.CLEAR_SEQUENCE_TASK, &seq);

                            Robs[0].Teller.AlwaysKeepTask = true;

                        }

                        Robs[0].Objective_Index = 4;

                    }
                    break;
                case 4:
                    {

                        if (!Robs[0].Teller_Door_Open)
                        {
                            if (Robs[0].Teller.Position.DistanceTo(FleecaDoor_Locations[Robs[0].List_Index]) < 1.2f)
                            {
                                Prop[] prop = World.GetNearbyProps(FleecaDoor_Locations[Robs[0].List_Index], 0.5f);
                                if (prop.Count() > 0)
                                {
                                    for (int i = 0; i < prop.Count(); i++)
                                    {
                                        if (prop[i].Model == -131754413)
                                        {
                                            prop[i].Heading = FleecaDoor_Heading[Robs[0].List_Index] + 90.0f;
                                            Robs[0].Teller_Door_Open = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (Robs[0].Teller.Position.DistanceTo(FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index]) < .5f)
                        {
                            Robs[0].Objective_Index = 5;
                            if (Robs[0].Teller.CurrentBlip != null)
                            {
                                Robs[0].Teller.CurrentBlip.Remove();
                            }
                        }

                        UI.ShowSubtitle("~y~Lead ~w~the ~b~Teller ~w~to the ~g~Vault.", 30000);

                        Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);



                        Game.Player.WantedLevel = 0;

                    }
                    break;
                case 5:
                    {
                        Robs[0].Teller.Task.ClearAllImmediately();

                        unsafe
                        {
                            int seq = 0;
                            GTA.Native.Function.Call(Hash.OPEN_SEQUENCE_TASK, &seq);
                            Function.Call(Hash.TASK_ACHIEVE_HEADING, 0, Teller_Heading[Robs[0].List_Index] - 95.0f, 1000);

                            Function.Call(Hash.TASK_PLAY_ANIM, 0, "amb@prop_human_atm@female@idle_a", "idle_b", 4.0f, 1.0f, 1100, 0, .0f, 0, 0, 0);
                            Function.Call(Hash.TASK_PLAY_ANIM, 0, "amb@prop_human_atm@female@idle_a", "idle_b", 4.0f, 1.0f, 1300, 0, .0f, 0, 0, 0);
                            Function.Call(Hash.TASK_PLAY_ANIM, 0, "amb@prop_human_atm@female@idle_a", "idle_b", 1.0f, 1.0f, 1100, 0, .0f, 0, 0, 0);
                            Function.Call(Hash.TASK_PLAY_ANIM, 0, "amb@prop_human_atm@female@idle_a", "idle_b", 1.0f, 1.0f, 1100, 0, .0f, 0, 0, 0);

                            GTA.Native.Function.Call(Hash.SET_SEQUENCE_TO_REPEAT, seq, false);
                            GTA.Native.Function.Call(Hash.CLOSE_SEQUENCE_TASK, seq);
                            GTA.Native.Function.Call(Hash.TASK_PERFORM_SEQUENCE, Robs[0].Teller, seq);
                            GTA.Native.Function.Call(Hash.CLEAR_SEQUENCE_TASK, &seq);
                            Robs[0].Teller.AlwaysKeepTask = true;
                            Robs[0].Objective_Index = 6;
                            UI.ShowSubtitle("~r~Force ~w~the ~b~Teller ~w~to open the ~g~Vault.", 5000);
                        }

                    }
                    break;

                case 6:
                    {
                        while (Function.Call<int>(Hash.GET_SEQUENCE_PROGRESS, Robs[0].Teller) != -1)
                        {
                            Wait(500);
                            if (Function.Call<int>(Hash.GET_SEQUENCE_PROGRESS, Robs[0].Teller) == 0)
                            {
                                Robs[0].Teller.FreezePosition = true;
                            }
                        }

                        Robs[0].Teller.FreezePosition = false;

                        Robs[0].Objective_Index = 7;

                        if (Robs[0].Objective_Index > -1 && Robs[0].Objective_Index < 8 && Robs[0].Teller.IsAlive == false && Robs[0].Faild == false)
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ScreenFlash", "MissionFailedSounds", 1);

                            Function.Call(Hash.PLAY_MISSION_COMPLETE_AUDIO, "GENERIC_FAILED");

                            WhoDied = "THE ~b~TELLER ";

                            BankAvalible[Robs[0].List_Index] = false;

                            Robs[0].Faild = true;

                            run = true;
                            step = 0;

                            Robs[0].Objective_Index = -1;

                        }

                    }
                    break;
                case 7:
                    {
                        Function.Call(Hash.PLAY_SOUND_FROM_COORD, Robs[0].Vault_Sound, "vault_unlock", FleecaVaultDoor_Locations[Robs[0].List_Index].X, FleecaVaultDoor_Locations[Robs[0].List_Index].Y, FleecaVaultDoor_Locations[Robs[0].List_Index].Z, "dlc_heist_fleeca_bank_door_sounds", 0, 0, 0);

                        Prop[] prop = World.GetNearbyProps(new Vector3(FleecaVaultDoor_Locations[Robs[0].List_Index].X, FleecaVaultDoor_Locations[Robs[0].List_Index].Y, FleecaVaultDoor_Locations[Robs[0].List_Index].Z - 1.2f), 1.5f);

                        if (prop.Count() > 0)
                        {
                            for (int i = 0; i < prop.Count(); i++)
                            {
                                if (prop[i].Model == -63539571)
                                {
                                    prop[i].FreezePosition = false;

                                    Function.Call(Hash.PLAY_ENTITY_ANIM, prop[i], "bank_vault_door_opens", "anim@heists@fleeca_bank@bank_vault_door", 4.0f, 0, 1, 0, 0f, 8);

                                    prop[i].HasCollision = false;
                                    Wait(1000);
                                    Function.Call(Hash.PLAY_SOUND_FROM_COORD, Robs[0].Alarm_Sound, "Burglar_Bell", FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].X, FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].Y, FleecaVaultDoor_KeyPad_Locations[Robs[0].List_Index].Z, "Generic_Alarms", 0, 0, 0);

                                    prop[i].FreezePosition = false;

                                    prop[i].MarkAsNoLongerNeeded();

                                }
                            }
                        }

                        Robs[0].Teller.Task.Cower(-1);

                        Robs[0].Objective_Index = 8;

                        Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);

                        Game.Player.WantedLevel = 0;

                    }
                    break;


                case 8:
                    {
                        if (!Robs[0].Vault_Door_Open)
                        {
                            Robs[0].Vault_Door_Open = true;
                            Robs[0].Objective_Index = 9;
                        }
                    }
                    break;
                case 9:
                    {

                        Robs[0].Objective_Index = 10;
                    }
                    break;
                case 10:
                    {
                        Random rnd = new Random();
                        int var1 = rnd.Next(1, 3);
                        if (CashEnabled == true)
                        {
                            if (var1 == 1)
                            {
                                var VaultCash = new Model("prop_cash_crate_01");
                                VaultCash.Request(250);

                                // Check the model is valid
                                if (VaultCash.IsInCdImage && VaultCash.IsValid)
                                {
                                    // Ensure the model is loaded before we try to create it in the world
                                    while (!VaultCash.IsLoaded) Script.Wait(50);

                                    // Create the prop in the world
                                    Prop cash = World.CreateProp(VaultCash, new Vector3(FleecaVaultCash_Locations[Robs[0].List_Index].X, FleecaVaultCash_Locations[Robs[0].List_Index].Y, FleecaVaultCash_Locations[Robs[0].List_Index].Z), new Vector3(0.0f, 0.0f, 0.0f), true, false);
                                    //cash.Position = new Vector3(FleecaVaultCash_Locations[Robs[0].List_Index].X, FleecaVaultCash_Locations[Robs[0].List_Index].Y, FleecaVaultCash_Locations[Robs[0].List_Index].Z);
                                    Robs[0].VaultCash = cash;
                                    cash.FreezePosition = true;
                                    Robs[0].Objective_Index = 11;
                                }

                            }
                            if (var1 == 2)
                            {
                                Robs[0].Objective_Index = 13;
                            }
                        }
                        if (CashEnabled == false)
                        {
                            Robs[0].Objective_Index = 13;
                        }
                    }
                    break;
                case 11:
                    {
                        //Blip blip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, FleecaVaultCash_Locations[Robs[0].List_Index].X, FleecaVaultCash_Locations[Robs[0].List_Index].Y, FleecaVaultCash_Locations[Robs[0].List_Index].Z);
                        //Robs[0].Cash_LocationBlip = blip;
                        //Robs[0].Cash_LocationBlip.Sprite = BlipSprite.DollarSignCircled;
                        //Robs[0].Cash_LocationBlip.Color = BlipColor.Green2;
                        //Robs[0].Cash_LocationBlip.Scale = .70f;

                        UI.ShowSubtitle("~y~Pickup ~w~the ~g~cash.", 000);

                        World.DrawMarker(MarkerType.VerticalCylinder, FleecaCash_Locations[Robs[0].List_Index], new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(.5f, .5f, 0.5f), Color.FromArgb(0, 255, 0), false, true, 2, false, "", "", false);

                        if (Game.Player.Character.Position.DistanceTo(FleecaCash_Locations[Robs[0].List_Index]) < 1.5f)
                        {
                            bool D_Pad_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 75);
                            bool key_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 175);

                            DisplayHelpTextThisFrame("~INPUT_FRONTEND_RIGHT~ to grab the cash");
                            if (D_Pad_Right || key_Right)
                            {
                                //Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@heists@ornate_bank@grab_cash", "grab", 1.0f, .01f, 10000, 0, .0f, 1, 1, 1);
                                Function.Call(Hash.PLAY_SOUND_FRONTEND, Robs[0].Checkpoint_Sound, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);
                                Robs[0].Objective_Index = 12;

                            }
                        }
                    }
                    break;
                case 12:
                    {
                        Prop[] prop = World.GetNearbyProps(FleecaVaultCash_Locations[Robs[0].List_Index], 0.5f);
                        if (prop.Count() > 0)
                        {
                            for (int i = 0; i < prop.Count(); i++)
                            {
                                if (prop[i].Model == -464691988)
                                {
                                    Robs[0].VaultCash = prop[i];
                                    Robs[0].VaultCash.MarkAsNoLongerNeeded();
                                    Robs[0].VaultCash.Position = new Vector3(0.0f, 0.0f, 0.0f);
                                }
                            }
                        }
                        Robs[0].Objective_Index = 16;
                    }
                    break;
                case 13:
                    {
                        //Blip drillblip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, FleecaVaultDrill_Locations[Robs[0].List_Index].X, FleecaVaultDrill_Locations[Robs[0].List_Index].Y, FleecaVaultDrill_Locations[Robs[0].List_Index].Z);
                        //Robs[0].Drill_LocationBlip = drillblip;
                        //Robs[0].Drill_LocationBlip.Sprite = BlipSprite.DevinDollarSign2;
                        //Robs[0].Drill_LocationBlip.Color = BlipColor.Green2;
                        //Robs[0].Drill_LocationBlip.Scale = .75f;
                        if (DrillEnabled == true)
                        {
                            UI.ShowSubtitle("~g~Go ~w~to the ~y~deposit box.", 000);

                            World.DrawMarker(MarkerType.VerticalCylinder, FleecaVaultDrill_Locations[Robs[0].List_Index], new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(.5f, .5f, 0.5f), Color.FromArgb(0, 255, 0), false, true, 2, false, "", "", false);

                            if (Game.Player.Character.Position.DistanceTo(FleecaVaultDrill_Locations[Robs[0].List_Index]) < 1.5f)
                            {
                                bool D_Pad_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 75);
                                bool key_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 175);

                                DisplayHelpTextThisFrame("~INPUT_FRONTEND_RIGHT~ to dril deposit box");
                                if (D_Pad_Right || key_Right)
                                {
                                    Robs[0].Objective_Index = 14;
                                }
                            }
                            Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);

                            Game.Player.WantedLevel = 0;
                        }
                        if (DrillEnabled == false)
                        {
                            Robs[0].Objective_Index = 10;
                        }
                    }
                    break;
                case 14:
                    {
                        if (Drill == null || Drill.Drilling == false)
                        {
                            if (Robs[0].Drill_LocationBlip != null)
                            {
                                Robs[0].Drill_LocationBlip.Remove();
                            }

                            Game.Player.Character.FreezePosition = true;

                            Driller rob = new Driller();
                            Drill = rob;

                            Drill.CamMode = Function.Call<int>(Hash.GET_FOLLOW_PED_CAM_VIEW_MODE);

                            Function.Call(Hash.SET_FOLLOW_PED_CAM_VIEW_MODE, 1);

                            Game.Player.Character.Position = FleecaVaultDrill_Locations[Robs[0].List_Index];
                            Game.Player.Character.Heading = FleecaVaultDrill_Heading[Robs[0].List_Index];

                            Game.Player.Character.FreezePosition = true;

                            Function.Call(Hash.SET_FOLLOW_PED_CAM_VIEW_MODE, Drill.CamMode);

                            Robs[0].Objective_Index = 15;
                        }

                    }
                    break;
                case 15:
                    {
                        bool D_Pad_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 74);
                        bool key_Right = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 2, 174);

                        DisplayHelpTextThisFrame("~INPUT_FRONTEND_LEFT~ to quit drilling");

                        if (key_Right)
                        {
                            if (Drill.Drilling)
                            {
                                Drill.RemoveDriller();

                                if (Robs[0].Drill_LocationBlip.Exists() == false)
                                {
                                    Game.Player.Character.FreezePosition = false;

                                    Blip blip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, FleecaVaultDrill_Locations[Robs[0].List_Index].X, FleecaVaultDrill_Locations[Robs[0].List_Index].Y, FleecaVaultDrill_Locations[Robs[0].List_Index].Z);
                                    Robs[0].Drill_LocationBlip = blip;
                                }
                            }

                            Robs[0].Objective_Index = 16;

                        }

                        Function.Call(Hash.SET_FAKE_WANTED_LEVEL, WantedLevel);

                        Game.Player.WantedLevel = 0;

                        if (Robs[0].Drilling_Done)
                        {
                            Function.Call(Hash.SET_FAKE_WANTED_LEVEL, 0);
                            if (Game.Player.WantedLevel <= WantedLevel)
                            {
                                Game.Player.WantedLevel = WantedLevel;

                            }
                            unsafe
                            {
                                int seq = 0;
                                GTA.Native.Function.Call(Hash.OPEN_SEQUENCE_TASK, &seq);
                                Function.Call(Hash.TASK_PLAY_ANIM, 0, "anim@heists@fleeca_bank@drilling", "outro", 1.0f, .01f, 10000, 0, .0f, 1, 1, 1);
                                GTA.Native.Function.Call(Hash.SET_SEQUENCE_TO_REPEAT, seq, false);
                                GTA.Native.Function.Call(Hash.CLOSE_SEQUENCE_TASK, seq);
                                GTA.Native.Function.Call(Hash.TASK_PERFORM_SEQUENCE, Game.Player.Character, seq);
                                GTA.Native.Function.Call(Hash.CLEAR_SEQUENCE_TASK, &seq);
                                Robs[0].Teller.AlwaysKeepTask = true;
                            }
                            Wait(2500);
                            Robs[0].BoxDoor.Heading -= 90.0f;
                            Wait(500);
                            Robs[0].CashEnvelope.Delete();
                            Wait(2500);
                            Robs[0].BoxDoor.Heading += 90.0f;
                            Wait(1000);
                            Drill.RemoveDriller();
                            Game.Player.Character.FreezePosition = false;

                            //Function.Call(Hash.SET_FAKE_WANTED_LEVEL, 0);
                            //if (Game.Player.WantedLevel <= WantedLevel)
                            //{
                            //    Game.Player.WantedLevel = WantedLevel;

                            //}
                            Robs[0].Objective_Index = 16;
                        }
                    }
                    break;
                case 16:
                    {
                        UI.ShowSubtitle("~y~Leave ~w~the ~g~bank.", 000);
                        Function.Call(Hash.SET_FAKE_WANTED_LEVEL, 0);

                        Game.Player.WantedLevel = WantedLevel;
                        if (Game.Player.Character.Position.DistanceTo(EnterExitBank_Locations[Robs[0].List_Index]) < 2.0f)
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, Robs[0].Checkpoint_Sound, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);

                            Vehicle lastVehicle = Function.Call<Vehicle>((Hash)0xB2D06FAEDE65B577);// GET_LAST_DRIVEN_VEHICLE
                            if (lastVehicle != null)
                            {
                                Function.Call(Hash.SET_VEHICLE_IS_WANTED, lastVehicle, true);
                            }

                            Robs[0].Objective_Index = 17;
                        }
                    }
                    break;

                case 17:
                    {
                        UI.ShowSubtitle("Lose The Cops.", 1000);
                        if (Game.Player.WantedLevel < 1)
                        {
                            Robs[0].Objective_Index = 18;
                            Passed_Index = 0;
                            BankAvalible[Robs[0].List_Index] = false;
                        }
                    }
                    break;
                case 18:
                    {
                        Robs[0].Objective_Index = -1;
                    }
                    break;

            }

            if (Robs[0].Objective_Index > -1 && Robs[0].Objective_Index < 7 && Robs[0].Teller.IsAlive == false && Robs[0].Faild == false)
            {
                BankAvalible[Robs[0].List_Index] = false;

                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ScreenFlash", "MissionFailedSounds", 1);
                Function.Call(Hash.PLAY_MISSION_COMPLETE_AUDIO, "GENERIC_FAILED");
                WhoDied = "THE ~b~TELLER ";
                Robs[0].Faild = true;
                run = true;
                step = 0;
                Robs[0].RemoveRob();
                Robs.RemoveAt(0);
            }

            if (Game.Player.IsDead)
            {
                while (!Game.IsScreenFadedOut)
                {
                    Wait(10);
                }
                while (Game.IsScreenFadedOut)
                {
                    Wait(4000);
                }

                BankAvalible[Robs[0].List_Index] = false;

                Function.Call(Hash.PLAY_MISSION_COMPLETE_AUDIO, "GENERIC_FAILED");

                WhoDied = "YOU ";
                Robs[0].Faild = true;
                run = true;
                step = 0;
                Robs[0].RemoveRob();
                Robs.RemoveAt(0);
            }

        }

        if (Function.Call<int>(Hash.GET_CLOCK_HOURS) > 18 || Function.Call<int>(Hash.GET_CLOCK_HOURS) < 7)
        {
            for (int i = 0; i < BankAvalible.Count; i++)
            {
                if (BankAvalible[i] == false)
                {
                    BankAvalible[i] = true;
                }
            }
        }

        if (Robs.Count < 1 && Function.Call<int>(Hash.GET_CLOCK_HOURS) >= 7 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 18)
        {
            for (int i = 0; i < Bank_Locations.Count; i++)
            {
                if (BankAvalible[i] == true && Game.Player.WantedLevel == 0)
                {
                }
                else
                {
                }
            }
        }



        switch (Passed_Index)
        {
            case 0:
                {
                    Scale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "MIDSIZED_MESSAGE");
                    time = (int)Game.GameTime + 1500;
                    Passed_Index = 1;
                }
                break;

            case 1:
                {
                    if ((int)Game.GameTime > time)
                    {
                        if (Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Scale))
                        {
                            Function.Call(Hash._START_SCREEN_EFFECT, "SuccessNeutral", 8500, false);

                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CHECKPOINT_PERFECT", "HUD_MINI_GAME_SOUNDSET", 1);
                            Function.Call(Hash.PLAY_MISSION_COMPLETE_AUDIO, "MICHAEL_BIG_01");

                            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Scale, "SHOW_SHARD_MIDSIZED_MESSAGE");

                            if (Robs[0].List_Index == 0)
                            {
                                if (!BankRobbed1)
                                {
                                    KeysData.SetValue<bool>("settings", "BankRobbed1", true);
                                    KeysData.Save();
                                    BankRobbed1 = true;
                                }
                            }
                            if (Robs[0].List_Index == 1)
                            {
                                if (!BankRobbed2)
                                {
                                    KeysData.SetValue("settings", "BankRobbed2", "True");
                                    KeysData.Save();
                                    BankRobbed2 = true;
                                }
                            }
                            if (Robs[0].List_Index == 2)
                            {
                                if (!BankRobbed3)
                                {
                                    KeysData.SetValue("settings", "BankRobbed3", "True");
                                    KeysData.Save();
                                    BankRobbed3 = true;
                                }
                            }
                            if (Robs[0].List_Index == 3)
                            {
                                if (!BankRobbed4)
                                {
                                    KeysData.SetValue("settings", "BankRobbed4", "True");
                                    KeysData.Save();
                                    BankRobbed4 = true;
                                }
                            }
                            if (Robs[0].List_Index == 4)
                            {
                                if (!BankRobbed5)
                                {
                                    KeysData.SetValue("settings", "BankRobbed5", "True");
                                    KeysData.Save();
                                    BankRobbed5 = true;
                                }
                            }
                            if (Robs[0].List_Index == 5)
                            {
                                if (!BankRobbed6)
                                {
                                    KeysData.SetValue("settings", "BankRobbed6", "True");
                                    KeysData.Save();
                                    BankRobbed6 = true;
                                }
                            }

                            int banksRobbed = 0;

                            if (BankRobbed1)
                            {
                                banksRobbed += 1;
                            }
                            if (BankRobbed2)
                            {
                                banksRobbed += 1;
                            }
                            if (BankRobbed3)
                            {
                                banksRobbed += 1;
                            }
                            if (BankRobbed4)
                            {
                                banksRobbed += 1;
                            }
                            if (BankRobbed5)
                            {
                                banksRobbed += 1;
                            }
                            if (BankRobbed6)
                            {
                                banksRobbed += 1;
                            }

                            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
                            Function.Call((Hash)0x6C188BE134E074AA, "~g~ROBBERY SUCCESS");
                            Function.Call(Hash._END_TEXT_COMPONENT);


                            int cash = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, MinCash, MaxCash); // 

                            Game.Player.Money += cash;
                            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
                            Function.Call((Hash)0x6C188BE134E074AA, "Banks Robbed " + banksRobbed.ToString() + "/6~n~" + "Money earned  " + cash.ToString());
                            Function.Call(Hash._END_TEXT_COMPONENT);

                            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

                            time = (int)Game.GameTime + 10000; // time to display message

                            Passed_Index = 2;
                        }
                    }

                }
                break;

            case 2:
                {
                    if (Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Scale))
                    {
                        if ((int)Game.GameTime <= time)
                        {
                            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Scale, 255, 255, 255, 255);
                        }
                        else if ((int)Game.GameTime < time + 100)
                        {
                            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Scale, "SHARD_ANIM_OUT");
                            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 1);// color 0,1=white 2=black 3=grey 6,7,8=red 9,10,11=blue 12,13,14=yellow 15,16,17=orange 18,19,20=green 21,22,23=purple 
                            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, .33f);

                            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                            time -= 100;
                        }
                        else if ((int)Game.GameTime < time + 2000)
                        {
                            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Scale, 255, 255, 255, 255);
                        }
                        else
                        {
                            Passed_Index = 3;
                        }
                    }
                }
                break;
            case 3:
                {
                    unsafe
                    {
                        int b = Scale;

                        Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &b);

                        Passed_Index = -1;
                        if (Robs.Count > 0)
                        {
                            Robs[0].RemoveRob();
                            Robs.RemoveAt(0);
                        }
                    }
                }
                break;
        }


        if (run)
        {
            switch (step)
            {
                case 0:
                    {
                        Scale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "MIDSIZED_MESSAGE");

                        time = (int)Game.GameTime + 1500; // time before message is displayed
                        step = 1;

                    }
                    break;

                case 1:
                    {
                        if ((int)Game.GameTime > time)
                        {
                            if (Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Scale))
                            {
                                Function.Call(Hash._START_SCREEN_EFFECT, "SuccessNeutral", 8000, false);

                                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Scale, "SHOW_SHARD_MIDSIZED_MESSAGE");

                                Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
                                Function.Call((Hash)0x6C188BE134E074AA, "ROBBERY FAILED");
                                Function.Call(Hash._END_TEXT_COMPONENT);
                                Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
                                Function.Call((Hash)0x6C188BE134E074AA, WhoDied + "~w~DIED");
                                Function.Call(Hash._END_TEXT_COMPONENT);
                                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 6);// color 0,1=white 2=black 3=grey 6,7,8=red 9,10,11=blue 12,13,14=yellow 15,16,17=orange 18,19,20=green 21,22,23=purple 

                                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

                                time = (int)Game.GameTime + 8000; // time to display message
                                step = 2;
                            }
                        }

                    }
                    break;

                case 2:
                    {
                        if (Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Scale))
                        {
                            if ((int)Game.GameTime <= time)
                            {
                                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Scale, 255, 255, 255, 255);
                            }
                            else if ((int)Game.GameTime < time + 100)
                            {
                                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Scale, "SHARD_ANIM_OUT");
                                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 1);// color 0,1=white 2=black 3=grey 6,7,8=red 9,10,11=blue 12,13,14=yellow 15,16,17=orange 18,19,20=green 21,22,23=purple 
                                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, .33f);

                                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                                time -= 100;
                            }
                            else if ((int)Game.GameTime < time + 2000)
                            {
                                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Scale, 255, 255, 255, 255);
                            }
                            else
                            {
                                step = 3;
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        unsafe
                        {
                            int b = Scale;

                            Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &b);

                            step = -1;
                        }

                        if (Robs.Count > 0)
                        {
                            Robs[0].RemoveRob();
                            Robs.RemoveAt(0);
                        }

                    }
                    break;
            }
        }


        if (Drill != null)
        {

            if (Game.Player.IsDead)
            {
                if (Drill.Drilling)
                {
                    Drill.RemoveDriller();
                }
            }

            if (Drill.Drilling)
            {
                UI.ShowSubtitle("Drill the ~g~safety deposit box", 1000);

                Function.Call(Hash.DISABLE_CONTROL_ACTION, 2, 37); // left button weapon wheel
                Function.Call(Hash.DISABLE_CONTROL_ACTION, 2, 27); // dpad up phone

                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Drill.ScaleFormDrilling, 255, 255, 255, 255);

                float leftStick = 0;  // used to set drill position
                float rightTrigger = 0;  // used to set drill speed

                if (Game.CurrentInputMode == InputMode.GamePad)
                {
                    leftStick = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 2, 32); // get drill position with controler left stick

                    rightTrigger = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 2, 24); // get input for drill speed
                    Drill.DrillSpeed = rightTrigger; // set drill speed
                }
                if (Game.CurrentInputMode == InputMode.MouseAndKeyboard)  // if using Mouse And Keyboard
                {
                    float mouse = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 2, 240); // mouse up and down (cam) 

                    mouse = 0.0f - mouse + .9f;

                    leftStick = mouse;

                    if (Function.Call<bool>(Hash.IS_CONTROL_JUST_PRESSED, 2, 241)) // wheel up   // get input for drill speed
                    {
                        if (Drill.DrillSpeed < 1.0f)
                        {
                            Drill.DrillSpeed += .125f;
                        }
                        else
                        {
                            Drill.DrillSpeed = 1.0f;
                        }
                    }
                    if (Function.Call<bool>(Hash.IS_CONTROL_JUST_PRESSED, 2, 242)) // wheel down   // get input for drill speed
                    {
                        if (Drill.DrillSpeed > .0f)
                        {
                            Drill.DrillSpeed -= .125f;
                        }
                        else
                        {
                            Drill.DrillSpeed = .0f;
                        }
                    }
                }


                if (Drill.ResetTime < (int)Game.GameTime)
                {
                    if (Drill.DrillSpeed > 0)
                    {
                        if (Function.Call<bool>(Hash.HAS_SOUND_FINISHED, Drill.Sound))
                        {
                            Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, Drill.Sound, "Drill", Drill.Drill_Prop, "DLC_HEIST_FLEECA_SOUNDSET", 1, 0);
                        }
                    }
                    else
                    {
                        Function.Call(Hash.STOP_SOUND, Drill.Sound);
                    }

                    if (Drill.DrillPosition == 0 && !Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_straight_idle", 3))
                    {
                        Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_straight_idle", 6.0f, .01f, -1, 0, .0f, 1, 1, 1);
                    }

                    if (Drill.DrillPosition > 0 && !Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_right_end", 3))
                    {
                        Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_right_end", 3.0f, .01f, -1, 0, .0f, 1, 1, 1);
                    }


                    if (Drill.DrillTemperature < 1.0f)
                    {
                        if (Drill.DrillPosition > Drill.DrillDepth - .2f)
                        {
                            if (Drill.DrillSpeed > .2f & Drill.DrillSpeed < .8f)
                            {
                                Drill.DrillDepth += .0003f;
                            }

                            if (Drill.DrillSpeed > .3f & Drill.DrillSpeed < .7f)
                            {
                                Drill.DrillDepth += .0003f;
                            }
                            if (Drill.DrillDepth > .776f)
                            {
                                if (Drill.DrillSpeed > .2f & Drill.DrillSpeed < .7f)
                                {
                                    Drill.DrillDepth += .005f;
                                }
                            }
                        }
                    }

                    if (leftStick > .75f && Drill.DrillSpeed > .0f || leftStick > 0 && Drill.DrillSpeed > .75f)
                    {
                        if (Drill.DrillPosition > Drill.DrillDepth - .2f)
                        {
                            if (Drill.DrillTemperature < 1.0f)
                            {
                                Drill.DrillTemperature += .015f;
                            }
                        }
                    }

                    if (Drill.DrillSpeed == 0 || Drill.DrillPosition == 0)
                    {
                        if (Drill.DrillTemperature > .0f)
                        {
                            Drill.DrillTemperature -= .005f;
                        }
                    }

                    if (Drill.DrillSpeed < .01f)
                    {
                        Function.Call(Hash.REMOVE_PARTICLE_FX, Drill.DrillFX, 0);
                    }

                    if (leftStick > 0)
                    {
                        if (Drill.DrillPosition < Drill.DrillDepth - .2f)
                        {
                            Drill.DrillPosition += .05f;
                        }
                        if (Drill.DrillPosition < Drill.DrillDepth)
                        {
                            Drill.DrillPosition += .01f;

                            if (Drill.DrillSpeed > 0)
                            {
                                Function.Call(Hash.REMOVE_PARTICLE_FX, Drill.DrillFX, 0);
                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "fm_mission_controler");
                                Drill.DrillFX = Function.Call<int>(Hash._START_PARTICLE_FX_LOOPED_ON_ENTITY_2, "scr_drill_debris", Drill.Drill_Prop, 0.0f, -0.55f, .01f, 90.0f, 90.0f, 90.0f, .8f, 0, 0, 0);
                                Function.Call(Hash.SET_PARTICLE_FX_LOOPED_EVOLUTION, Drill.DrillFX, "power", .7f, 0);
                            }
                            else
                            {
                                Function.Call(Hash.REMOVE_PARTICLE_FX, Drill.DrillFX, 0);
                            }
                        }
                    }
                    else
                    {
                        Drill.DrillPosition = .0f;
                        Function.Call(Hash.REMOVE_PARTICLE_FX, Drill.DrillFX, 0);
                    }

                    Function.Call(Hash.SET_VARIABLE_ON_SOUND, Drill.Sound, "DrillState", .0);

                    if (leftStick > Drill.DrillDepth && leftStick < Drill.DrillDepth + .2f)
                    {
                        Function.Call(Hash.SET_VARIABLE_ON_SOUND, Drill.Sound, "DrillState", .5);
                    }
                    if (leftStick > Drill.DrillDepth && leftStick > Drill.DrillDepth + .2f)
                    {
                        Function.Call(Hash.SET_VARIABLE_ON_SOUND, Drill.Sound, "DrillState", 1.0);
                    }


                    if (Drill.DrillDepth > .326f && !Drill.Pin_one)
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, Drill.PinSound, "Drill_Pin_Break", "DLC_HEIST_FLEECA_SOUNDSET", 1);
                        Drill.Pin_one = true;
                    }
                    if (Drill.DrillDepth > .476f && !Drill.Pin_two)
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, Drill.PinSound, "Drill_Pin_Break", "DLC_HEIST_FLEECA_SOUNDSET", 1);
                        Drill.Pin_two = true;
                    }
                    if (Drill.DrillDepth > .625f && !Drill.Pin_three)
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, Drill.PinSound, "Drill_Pin_Break", "DLC_HEIST_FLEECA_SOUNDSET", 1);
                        Drill.Pin_three = true;
                    }
                    if (Drill.DrillDepth > .776f && !Drill.Pin_four)
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, Drill.PinSound, "Drill_Pin_Break", "DLC_HEIST_FLEECA_SOUNDSET", 1);
                        Drill.Pin_four = true;
                    }

                    if (Drill.DrillTemperature > .99f || Drill.DrillPosition > Drill.DrillDepth + .3f)
                    {
                        if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, Drill.Sound))
                        {
                            Function.Call(Hash.SET_VARIABLE_ON_SOUND, Drill.Sound, "DrillState", .0);

                            Function.Call(Hash.STOP_SOUND, Drill.Sound);
                        }
                        if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, Drill.PinSound))
                        {
                            Function.Call(Hash.STOP_SOUND, Drill.PinSound);
                        }

                        Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, Drill.FailSound, "Drill_Jam", Game.Player.Character, "DLC_HEIST_FLEECA_SOUNDSET", 1, 20);

                        Drill.Fail();
                    }

                }

                Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, Drill.ScaleFormDrilling, "SET_TEMPERATURE", Drill.DrillTemperature, -1082130432, -1082130432, -1082130432, -1082130432);

                Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, Drill.ScaleFormDrilling, "SET_DRILL_POSITION", Drill.DrillPosition, -1082130432, -1082130432, -1082130432, -1082130432);

                Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, Drill.ScaleFormDrilling, "SET_SPEED", Drill.DrillSpeed, -1082130432, -1082130432, -1082130432, -1082130432);

                if (Robs.Count > 0)
                {
                    if (Drill.DrillPosition > .8f)
                    {
                        Function.Call(Hash.REMOVE_PARTICLE_FX, Drill.DrillFX, 0);

                        if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, Drill.Sound))
                        {
                            Function.Call(Hash.STOP_SOUND, Drill.Sound);
                        }

                        Drill.DrillTemperature = 0;
                        Drill.DrillSpeed = 0;
                        Robs[0].Drilling_Done = true;

                    }
                }
            }
        }
    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {

        KeysData = ScriptSettings.Load("scripts//RobFleeca.ini");

        Keys StartKey = KeysData.GetValue<Keys>("settings", "StartKey", Keys.U);


        if (e.KeyCode == StartKey)
        {
            if (Game.Player.WantedLevel < 1 && Robs.Count < 1 && Function.Call<int>(Hash.GET_CLOCK_HOURS) >= 7 && Function.Call<int>(Hash.GET_CLOCK_HOURS) < 18)  // 7am - 6pm
            {
                for (int i = 0; i < Bank_Locations.Count; i++)
                {
                    if (Bank_Locations[i].DistanceTo(Game.Player.Character.Position) < 12.0f && BankAvalible[i] == true)
                    {
                        Rob rob = new Rob(i);
                        Robs.Add(rob);
                    }
                }
            }
        }


    }

    public class Rob
    {
        public Rob(int list_index)
        {

            this.Objective_Index = 0;

            this.List_Index = list_index;

            this.Teller_Door_Open = false;

            this.Vault_Door_Open = false;

            this.Faild = false;

            this.Drilling_Done = false;

            this.TellerCower = false;

            this.Hostage3Cower = false;

            Function.Call(Hash.REQUEST_ANIM_DICT, "amb@prop_human_atm@female@idle_a");
            Function.Call(Hash.REQUEST_ANIM_DICT, "anim@heists@fleeca_bank@bank_vault_door");
            Function.Call(Hash.REQUEST_ANIM_DICT, "anim@heists@fleeca_bank@drilling");

            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "amb@prop_human_atm@female@idle_a") &&
               !Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@heists@fleeca_bank@bank_vault_door") &&
               !Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@heists@fleeca_bank@drilling") &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HEIST_FLEECA_DRILL", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HEIST_FLEECA_DRILL_2", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL_2", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "SAFE_CRACK", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_Biker_Cracked_Sounds", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HUD_MINI_GAME_SOUNDSET", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "MissionFailedSounds", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "dlc_heist_fleeca_bank_door_sounds", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "vault_door", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_SCRIPT_AUDIO_BANK, "Alarms", 0, -1) &&
               !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_HEIST_FLEECA_SOUNDSET", 0, -1))
            {
                Wait(100);
            }

            this.Alarm_Sound = -1;

            this.Vault_Sound = -1;

            this.Checkpoint_Sound = -1;

            this.Alarm_Sound = Function.Call<int>(Hash.GET_SOUND_ID);

            while (this.Alarm_Sound == -1)
            {
                Wait(0);
            }

            this.Vault_Sound = Function.Call<int>(Hash.GET_SOUND_ID);

            while (this.Vault_Sound == -1)
            {
                Wait(0);
            }

            this.Checkpoint_Sound = Function.Call<int>(Hash.GET_SOUND_ID);

            while (this.Checkpoint_Sound == -1)
            {
                Wait(0);
            }
        }

        public int Objective_Index;

        public int List_Index;

        public Ped Teller;

        public bool TellerCower;

        public Ped Hostage1;

        public Ped Hostage2;

        public Ped Hostage3;

        public bool Hostage3Cower;

        public Prop Cam1;

        public Prop Cam2;

        public Prop Cam3;

        public Prop Cam4;

        public Prop Cam5;

        public Prop BoxWall;

        public Prop BoxDoor;

        public Prop CashEnvelope;

        public Prop VaultCash;

        public bool Faild;

        public bool Drilling_Done;

        public Blip Drill_LocationBlip;

        public Blip Cash_LocationBlip;

        public bool Teller_Door_Open;

        public bool Vault_Door_Open;

        public int Alarm_Sound;

        public int Vault_Sound;

        public int Checkpoint_Sound;

        public void RemoveRob() // funtion to remove when canciling or complete
        {
            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.Alarm_Sound))
            {
                Function.Call(Hash.STOP_SOUND, this.Alarm_Sound);
            }

            //if (this.Alarm_Sound != 0) // release all sounds
            //{
            Function.Call(Hash.RELEASE_SOUND_ID, this.Alarm_Sound);
            //}

            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.Vault_Sound))
            {
                Function.Call(Hash.STOP_SOUND, this.Vault_Sound);
            }

            //if (this.Vault_Sound != 0) // release all sounds
            //{
            Function.Call(Hash.RELEASE_SOUND_ID, this.Vault_Sound);
            //}

            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.Checkpoint_Sound))
            {
                Function.Call(Hash.STOP_SOUND, this.Checkpoint_Sound);
            }

            //if (this.Checkpoint_Sound != 0) // release all sounds
            //{
            Function.Call(Hash.RELEASE_SOUND_ID, this.Checkpoint_Sound);
            //}

            //Function.Call(Hash.REMOVE_ANIM_DICT, "mp_heists@keypad@");

            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@heists@fleeca_bank@bank_vault_door");
            Function.Call(Hash.REMOVE_ANIM_DICT, "amb@prop_human_atm@female@idle_a");
            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@heists@fleeca_bank@drilling");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "SAFE_CRACK");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_Biker_Cracked_Sounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HUD_MINI_GAME_SOUNDSET");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "MissionFailedSounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "vault_door");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "Alarms");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "SAFE_CRACK");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_Biker_Cracked_Sounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HUD_MINI_GAME_SOUNDSET");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "MissionFailedSounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "dlc_heist_fleeca_bank_door_sounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "vault_door");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "Alarms");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL_2");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL_2");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_HEIST_FLEECA_SOUNDSET");

            Game.Player.WantedLevel = Function.Call<int>((Hash)0x4C9296CBCD1B971E);

            Function.Call(Hash.SET_FAKE_WANTED_LEVEL, 0);

            if (this.Teller.CurrentBlip != null)
            {
                this.Teller.CurrentBlip.Remove();
            }
            if (this.Teller != null)
            {
                this.Teller.MarkAsNoLongerNeeded();
            }

            if (this.Hostage1.CurrentBlip != null)
            {
                this.Hostage1.CurrentBlip.Remove();
            }
            if (this.Hostage1 != null)
            {
                this.Hostage1.MarkAsNoLongerNeeded();
            }

            if (this.Hostage2.CurrentBlip != null)
            {
                this.Hostage2.CurrentBlip.Remove();
            }
            if (this.Hostage2 != null)
            {
                this.Hostage2.MarkAsNoLongerNeeded();
            }

            if (this.Hostage3.CurrentBlip != null)
            {
                this.Hostage3.CurrentBlip.Remove();
            }
            if (this.Hostage3 != null)
            {
                this.Hostage3.MarkAsNoLongerNeeded();
            }

            if (this.Cam1 != null)
            {
                if (this.Cam1.CurrentBlip.Exists())
                {
                    this.Cam1.CurrentBlip.Remove();
                }
                this.Cam1.MarkAsNoLongerNeeded();
            }
            if (this.Cam2 != null)
            {
                if (this.Cam2.CurrentBlip.Exists())
                {
                    this.Cam2.CurrentBlip.Remove();
                }
                this.Cam2.MarkAsNoLongerNeeded();
            }
            if (this.Cam3 != null)
            {
                if (this.Cam3.CurrentBlip.Exists())
                {
                    this.Cam3.CurrentBlip.Remove();
                }
                this.Cam3.MarkAsNoLongerNeeded();
            }
            if (this.Cam4 != null)
            {
                if (this.Cam4.CurrentBlip.Exists())
                {
                    this.Cam4.CurrentBlip.Remove();
                }
                this.Cam4.MarkAsNoLongerNeeded();
            }
            if (this.Cam5 != null)
            {
                if (this.Cam5.CurrentBlip.Exists())
                {
                    this.Cam5.CurrentBlip.Remove();
                }
                this.Cam5.MarkAsNoLongerNeeded();
            }
            if (this.Drill_LocationBlip != null)
            {
                this.Drill_LocationBlip.Remove();
            }
            if (this.Cash_LocationBlip != null)
            {
                this.Cash_LocationBlip.Remove();
            }


            if (this.BoxWall != null)
            {
                this.BoxWall.MarkAsNoLongerNeeded();
            }
            if (this.BoxDoor != null)
            {
                this.BoxDoor.MarkAsNoLongerNeeded();
            }
            if (this.CashEnvelope != null)
            {
                this.CashEnvelope.MarkAsNoLongerNeeded();
            }

            this.Faild = false;
        }
    }


    public class Driller
    {

        public Driller()
        {
            Game.Player.Character.Weapons.Give(WeaponHash.Unarmed, 0, true, true);

            Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, false);

            Game.Player.Character.FreezePosition = true;

            int scale = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE_INSTANCE, "DRILLING");

            while (!Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, scale))
            {
                Wait(100);
            }

            this.ScaleFormDrilling = scale;

            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, scale, "SET_SPEED", .0f, -1082130432, -1082130432, -1082130432, -1082130432);
            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, scale, "SET_HOLE_DEPTH", .1f, -1082130432, -1082130432, -1082130432, -1082130432);
            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, scale, "SET_DRILL_POSITION", .0f, -1082130432, -1082130432, -1082130432, -1082130432);
            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_FLOAT_PARAMS, scale, "SET_TEMPERATURE", .0f, -1082130432, -1082130432, -1082130432, -1082130432);

            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "fm_mission_controler");

            Function.Call(Hash.REQUEST_ANIM_DICT, "anim@heists@fleeca_bank@drilling");

            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "anim@heists@fleeca_bank@drilling") &&
                !Function.Call<bool>(Hash.HAS_NAMED_PTFX_ASSET_LOADED, "fm_mission_controler") &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HEIST_FLEECA_DRILL", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HEIST_FLEECA_DRILL_2", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL_2", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "SAFE_CRACK", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_Biker_Cracked_Sounds", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "HUD_MINI_GAME_SOUNDSET", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "MissionFailedSounds", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "dlc_heist_fleeca_bank_door_sounds", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "vault_door", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_SCRIPT_AUDIO_BANK, "Alarms", 0, -1) &&
                !Function.Call<bool>(Hash.REQUEST_AMBIENT_AUDIO_BANK, "DLC_HEIST_FLEECA_SOUNDSET", 0, -1))
            {
                Wait(100);
            }

            this.Sound = Function.Call<int>(Hash.GET_SOUND_ID);

            this.PinSound = Function.Call<int>(Hash.GET_SOUND_ID);

            this.FailSound = Function.Call<int>(Hash.GET_SOUND_ID);

            var model = new Model("hei_prop_heist_drill");
            model.Request(10000);
            Prop drill = World.CreateProp(model, Game.Player.Character.Position, false, false);
            this.Drill_Prop = drill;
            drill.FreezePosition = true;
            drill.HasCollision = false;
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, drill, Game.Player.Character, Game.Player.Character.GetBoneIndex((Bone)28422), 0f, 0f, 0f, 0f, 0f, 0f, 0, 0, 0, 0, 2, 1);
            drill.IsInvincible = true;
            model.MarkAsNoLongerNeeded();

            Vector3 po = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1;

            Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_right_end", 1.0f, .1f, 2000, 0, .0f, 1, 1, 1);

            this.Drilling = true;

            this.DrillSpeed = 0.0f;

            this.DrillPosition = 0.0f;

            this.DrillDepth = 0.1f;

            this.DrillTemperature = 0.0f;

            this.Pin_one = false;

            this.Pin_two = false;

            this.Pin_three = false;

            this.Pin_four = false;

            this.Reset = false;

            this.CamMode = 1;

        }

        public int ScaleFormDrilling;

        public int DrillingSound;

        public bool Drilling;

        public Prop Drill_Prop;

        public Prop CashPickup;

        public bool Pin_one;

        public bool Pin_two;

        public bool Pin_three;

        public bool Pin_four;

        public bool Reset;

        public float DrillSpeed;

        public float DrillDepth;

        public float DrillPosition;

        public float DrillTemperature;

        public int Sound;

        public int PinSound;

        public int FailSound;

        public int DrillFX;

        public int ResetTime;

        public int CamMode;

        public void Fail()
        {
            Function.Call(Hash.REMOVE_PARTICLE_FX, this.DrillFX, 0);
            Function.Call(Hash.TASK_PLAY_ANIM, Game.Player.Character, "anim@heists@fleeca_bank@drilling", "drill_straight_fail", 1.0f, .1f, 2000, 0, .0f, 1, 1, 1);
            this.DrillPosition = 0.0f;
            this.DrillSpeed = 0.0f;
            this.DrillTemperature = 0.0f;
            this.Reset = false;
            this.ResetTime = (int)Game.GameTime + 1000; // set reset time so you cant contimue for 1 second
        }

        public void RemoveDriller()
        {
            Function.Call(Hash.ENABLE_CONTROL_ACTION, 2, 37); //weapon wheel
            Function.Call(Hash.ENABLE_CONTROL_ACTION, 2, 27); // dpad up phone

            Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, true);

            Game.Player.Character.FreezePosition = false;

            Game.Player.CanControlCharacter = true;

            Game.Player.Character.Task.ClearAllImmediately();

            if (this.Drill_Prop != null)
            {
                this.Drill_Prop.Delete();

            }
            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.Sound))
            {
                Function.Call(Hash.STOP_SOUND, this.Sound);
            }
            if (this.Sound != 0)
            {
                Function.Call(Hash.RELEASE_SOUND_ID, this.Sound);
            }

            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.PinSound))
            {
                Function.Call(Hash.STOP_SOUND, this.PinSound);
            }
            if (this.PinSound != 0)
            {
                Function.Call(Hash.RELEASE_SOUND_ID, this.PinSound);
            }

            if (!Function.Call<bool>(Hash.HAS_SOUND_FINISHED, this.FailSound))
            {
                Function.Call(Hash.STOP_SOUND, this.FailSound);
            }
            if (this.FailSound != 0)
            {
                Function.Call(Hash.RELEASE_SOUND_ID, this.FailSound);
            }


            Function.Call(Hash.REMOVE_PARTICLE_FX, this.DrillFX, 0);

            this.Drilling = false;

            this.Pin_one = false;

            this.Pin_two = false;

            this.Pin_three = false;

            this.Pin_four = false;

            unsafe
            {
                int d = this.ScaleFormDrilling;

                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &d);
            }

            //for (int i = 0; i < Function.Call<int>(Hash.GET_SOUND_ID) + 1; i++)
            //{
            //    Function.Call(Hash.STOP_SOUND, i);
            //    Function.Call(Hash.RELEASE_SOUND_ID, i);
            //}

            Function.Call(Hash._REMOVE_NAMED_PTFX_ASSET, "fm_mission_controler");

            Function.Call(Hash.REMOVE_ANIM_DICT, "anim@heists@fleeca_bank@drilling");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL_2");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL_2");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_HEIST_FLEECA_SOUNDSET");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "SAFE_CRACK");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_Biker_Cracked_Sounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HUD_MINI_GAME_SOUNDSET");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "MissionFailedSounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "dlc_heist_fleeca_bank_door_sounds");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "vault_door");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "Alarms");

            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "HEIST_FLEECA_DRILL_2");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_MPHEIST\\HEIST_FLEECA_DRILL_2");
            //Function.Call(Hash.RELEASE_NAMED_SCRIPT_AUDIO_BANK, "DLC_HEIST_FLEECA_SOUNDSET");
        }



    }

    void DisplayHelpTextThisFrame(string text) // used to display quit message. credit jedijosh920 for this function
    {
        Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
        Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
        Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
    }


}