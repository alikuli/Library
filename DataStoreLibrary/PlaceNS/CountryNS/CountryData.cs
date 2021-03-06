﻿
namespace DatastoreNS
{
    public static class CountryData
    {
        public static string[,] CountryDataArray()
        {
            string[,] a = new string[245, 3] {
                {"Andorra, Principality of " ,                                          "AD",   " +376 "},
                {"United Arab Emirates (UAE) (Former Trucial Oman, Trucial States)" ,   "AE", " +971 "},
                {"Afghanistan " ,                                                       "AF", " +93 "},
                {"Antigua and Barbuda" ,  "AG", " +1-268 "},
                {"Anguilla " ,  "AI", " +1-264 "},
                {"Albania " ,  "AL", " +355 "},
                {"Armenia" ,  "AM", " +374 "},
                {"Netherlands Antilles (Former Curacao and Dependencies)" ,  "AN", " +599 "},
                {"Angola" ,  "AO", " +244 "},
                {"Antarctica" ,  "AQ", " +672 "},
                {"Argentina " ,  "AR", " +54 "},
                {"American Samoa" ,  "AS", " +1-684 "},
                {"Austria" ,  "AT", " +43 "},
                {"Australia" ,  "AU", " +61 "},
                {"Aruba" ,  "AW", " +297 "},
                {"Azerbaijan or Azerbaidjan (Former Azerbaijan Soviet Socialist Republic)" ,  "AZ", " +994 "},
                {"Bosnia and Herzegovina " ,  "BA", " +387 "},
                {"Barbados " ,  "BB", " +1-246 "},
                {"Bangladesh (Former East Pakistan)" ,  "BD", " +880 "},
                {"Belgium " ,  "BE", " +32 "},
                {"Burkina Faso (Former Upper Volta)" ,  "BF", " +226 "},
                {"Bulgaria " ,  "BG", " +359 "},
                {"Bahrain, Kingdom of (Former Dilmun)" ,  "BH", " +973 "},
                {"Burundi (Former Urundi)" ,  "BI", " +257 "},
                {"Benin (Former Dahomey)" ,  "BJ", " +229 "},
                {"Bermuda " ,  "BM", " +1-441 "},
                {"Brunei (Negara Brunei Darussalam) " ,  "BN", " +673 "},
                {"Bolivia " ,  "BO", " +591 "},
                {"Brazil " ,  "BR", " +55 "},
                {"Bahamas, Commonwealth of The" ,  "BS", " +1-242 "},
                {"Bhutan, Kingdom of" ,  "BT", " +975 "},
                {"Bouvet Island (Territory of Norway)" ,  "BV", "  "},
                {"Botswana (Former Bechuanaland)" ,  "BW", " +267 "},
                {"Belarus (Former Belorussian [Byelorussian] Soviet Socialist Republic)" ,  "BY", " +375 "},
                {"Belize (Former British Honduras)" ,  "BZ", " +501 "},
                {"Canada " ,  "CA", " +1 "},
                {"Cocos (Keeling) Islands " ,  "CC", " +61 "},
                {"Congo, Democratic Republic of the (Former Zaire) " ,  "CD", " +243 "},
                {"Central African Republic " ,  "CF", " +236 "},
                {"Congo, Republic of the" ,  "CG", " +242 "},
                {"Switzerland " ,  "CH", " +41 "},
                {"Cote D'Ivoire (Former Ivory Coast) " ,  "CI", " +225 "},
                {"Cook Islands (Former Harvey Islands)" ,  "CK", " +682 "},
                {"Chile " ,  "CL", " +56 "},
                {"Cameroon (Former French Cameroon)" ,  "CM", " +237 "},
                {"China " ,  "CN", " +86 "},
                {"Colombia " ,  "CO", " +57 "},
                {"Costa Rica " ,  "CR", " +506 "},
                {"Czechoslavakia (Former) See CZ Czech Republic or Slovakia" ,  "CS", "  "},
                {"Cuba " ,  "CU", " +53 "},
                {"Cape Verde " ,  "CV", " +238 "},
                {"Christmas Island " ,  "CX", " +53 "},
                {"Cyprus " ,  "CY", " +357 "},
                {"Czech Republic" ,  "CZ", " +420 "},
                {"Germany " ,  "DE", " +49 "},
                {"Djibouti (Former French Territory of the Afars and Issas, French Somaliland)" ,  "DJ", " +253 "},
                {"Denmark " ,  "DK", " +45 "},
                {"Dominica " ,  "DM", " +1-767 "},
                {"Dominican Republic " ,  "DO", " +1-809 and +1-829   "},
                {"Algeria " ,  "DZ", " +213 "},
                {"Ecuador " ,  "EC", " +593  "},
                {"Estonia (Former Estonian Soviet Socialist Republic)" ,  "EE", " +372 "},
                {"Egypt (Former United Arab Republic - with Syria)" ,  "EG", " +20 "},
                {"Western Sahara (Former Spanish Sahara)" ,  "EH", "  "},
                {"Eritrea (Former Eritrea Autonomous Region in Ethiopia)" ,  "ER", " +291 "},
                {"Spain " ,  "ES", " +34 "},
                {"Ethiopia (Former Abyssinia, Italian East Africa)" ,  "ET", " +251 "},
                {"Finland " ,  "FI", " +358 "},
                {"Fiji " ,  "FJ", " +679 "},
                {"Falkland Islands (Islas Malvinas) " ,  "FK", " +500 "},
                {"Micronesia, Federated States of (Former Ponape, Truk, and Yap Districts - Trust Territory of the Pacific Islands)" ,  "FM", " +691 "},
                {"Faroe Islands " ,  "FO", " +298 "},
                {"France " ,  "FR", " +33 "},
                {"Gabon (Gabonese Republic)" ,  "GA", " +241 "},
                {"Great Britain (United Kingdom) " ,  "GB", "  "},
                {"United Kingdom (Great Britain / UK)" ,  "GB", " +44 "},
                {"Grenada " ,  "GD", " +1-473 "},
                {"Georgia (Former Georgian Soviet Socialist Republic)" ,  "GE", " +995 "},
                {"French Guiana or French Guyana " ,  "GF", " +594 "},
                {"Ghana (Former Gold Coast)" ,  "GH", " +233 "},
                {"Gibraltar " ,  "GI", " +350 "},
                {"Greenland " ,  "GL", " +299 "},
                {"Gambia, The " ,  "GM", " +220 "},
                {"Guinea (Former French Guinea)" ,  "GN", " +224 "},
                {"Guadeloupe" ,  "GP", " +590 "},
                {"Equatorial Guinea (Former Spanish Guinea)" ,  "GQ", " +240 "},
                {"Greece " ,  "GR", " +30 "},
                {"South Georgia and the South Sandwich Islands" ,  "GS", "  "},
                {"Guatemala " ,  "GT", " +502 "},
                {"Guam" ,  "GU", " +1-671 "},
                {"Guinea-Bissau (Former Portuguese Guinea)" ,  "GW", " +245 "},
                {"Guyana (Former British Guiana)" ,  "GY", " +592 "},
                {"Hong Kong " ,  "HK", " +852 "},
                {"Heard Island and McDonald Islands (Territory of Australia)" ,  "HM", "  "},
                {"Honduras " ,  "HN", " +504 "},
                {"Croatia (Hrvatska) " ,  "HR", " +385 "},
                {"Haiti " ,  "HT", " +509 "},
                {"Hungary " ,  "HU", " +36 "},
                {"Indonesia (Former Netherlands East Indies; Dutch East Indies)" ,  "ID", " +62 "},
                {"Ireland " ,  "IE", " +353 "},
                //{"Israel " ,  "IL", " +972 "},
                {"India " ,  "IN", " +91 "},
                {"British Indian Ocean Territory (BIOT)" ,  "IO", "  "},
                {"Iraq " ,  "IQ", " +964 "},
                {"Iran, Islamic Republic of" ,  "IR", " +98 "},
                {"Iceland " ,  "IS", " +354 "},
                {"Italy " ,  "IT", " +39 "},
                {"Jamaica " ,  "JM", " +1-876 "},
                {"Jordan (Former Transjordan)" ,  "JO", " +962 "},
                {"Japan " ,  "JP", " +81 "},
                {"Kenya (Former British East Africa)" ,  "KE", " +254 "},
                {"Kyrgyzstan (Kyrgyz Republic) (Former Kirghiz Soviet Socialist Republic)" ,  "KG", " +996 "},
                {"Cambodia, Kingdom of (Former Khmer Republic, Kampuchea Republic)" ,  "KH", " +855 "},
                {"Kiribati (Pronounced keer-ree-bahss) (Former Gilbert Islands)" ,  "KI", " +686 "},
                {"Comoros, Union of the " ,  "KM", " +269 "},
                {"Saint Kitts and Nevis (Former Federation of Saint Christopher and Nevis)" ,  "KN", " +1-869 "},
                {"Korea, Democratic People's Republic of (North Korea)" ,  "KP", " +850 "},
                {"Korea, Republic of (South Korea) " ,  "KR", " +82 "},
                {"Kuwait " ,  "KW", " +965 "},
                {"Cayman Islands " ,  "KY", " +1-345 "},
                {"Kazakstan or Kazakhstan (Former Kazakh Soviet Socialist Republic)" ,  "KZ", " +7 "},
                {"Lao People's Democratic Republic (Laos)" ,  "LA", " +856 "},
                {"Lebanon " ,  "LB", " +961 "},
                {"Saint Lucia " ,  "LC", " +1-758 "},
                {"Liechtenstein " ,  "LI", " +423 "},
                {"Sri Lanka (Former Serendib, Ceylon) " ,  "LK", " +94 "},
                {"Liberia " ,  "LR", " +231 "},
                {"Lesotho (Former Basutoland)" ,  "LS", " +266 "},
                {"Lithuania (Former Lithuanian Soviet Socialist Republic)" ,  "LT", " +370 "},
                {"Luxembourg " ,  "LU", " +352 "},
                {"Latvia (Former Latvian Soviet Socialist Republic)" ,  "LV", " +371 "},
                {"Libya (Libyan Arab Jamahiriya)" ,  "LY", " +218 "},
                {"Morocco " ,  "MA", " +212 "},
                {"Monaco, Principality of" ,  "MC", " +377 "},
                {"Moldova, Republic of" ,  "MD", " +373 "},
                {"Madagascar (Former Malagasy Republic)" ,  "MG", " +261 "},
                {"Marshall Islands (Former Marshall Islands District - Trust Territory of the Pacific Islands)" ,  "MH", " +692 "},
                {"Macedonia, The Former Yugoslav Republic of" ,  "MK", " +389 "},
                {"Mali (Former French Sudan and Sudanese Republic) " ,  "ML", " +223 "},
                {"Myanmar, Union of (Former Burma)" ,  "MM", " +95 "},
                {"Mongolia (Former Outer Mongolia)" ,  "MN", " +976 "},
                {"Macau " ,  "MO", " +853 "},
                {"Northern Mariana Islands (Former Mariana Islands District - Trust Territory of the Pacific Islands)" ,  "MP", " +1-670 "},
                {"Martinique (French) " ,  "MQ", " +596 "},
                {"Mauritania " ,  "MR", " +222 "},
                {"Montserrat " ,  "MS", " +1-664 "},
                {"Malta " ,  "MT", " +356 "},
                {"Mauritius " ,  "MU", " +230 "},
                {"Maldives " ,  "MV", " +960 "},
                {"Malawi (Former British Central African Protectorate, Nyasaland)" ,  "MW", " +265 "},
                {"Mexico " ,  "MX", " +52 "},
                {"Malaysia " ,  "MY", " +60 "},
                {"Mozambique (Former Portuguese East Africa)" ,  "MZ", " +258 "},
                {"Namibia (Former German Southwest Africa, South-West Africa)" ,  "NA", " +264 "},
                {"New Caledonia " ,  "NC", " +687 "},
                {"Niger " ,  "NE", " +227 "},
                {"Norfolk Island " ,  "NF", " +672 "},
                {"Nigeria " ,  "NG", " +234 "},
                {"Nicaragua " ,  "NI", " +505 "},
                {"Netherlands " ,  "NL", " +31 "},
                {"Norway " ,  "NO", " +47 "},
                {"Nepal " ,  "NP", " +977 "},
                {"Nauru (Former Pleasant Island)" ,  "NR", " +674 "},
                {"Niue (Former Savage Island)" ,  "NU", " +683 "},
                {"New Zealand (Aotearoa) " ,  "NZ", " +64 "},
                {"Oman, Sultanate of (Former Muscat and Oman)" ,  "OM", " +968 "},
                {"Panama " ,  "PA", " +507 "},
                {"Peru " ,  "PE", " +51 "},
                {"French Polynesia (Former French Colony of Oceania)" ,  "PF", " +689 "},
                {"Papua New Guinea (Former Territory of Papua and New Guinea)" ,  "PG", " +675 "},
                {"Philippines " ,  "PH", " +63 "},
                {"Pakistan (Former West Pakistan)" ,  "PK", " +92 "},
                {"Poland " ,  "PL", " +48 "},
                {"Saint Pierre and Miquelon " ,  "PM", " +508 "},
                {"Pitcairn Island" ,  "PN", "  "},
                {"Puerto Rico " ,  "PR", "+1-939 "},
                {"Palestinian State (Proposed)" ,  "PS", " +970 "},
                {"Portugal " ,  "PT", " +351 "},
                {"Palau (Former Palau District - Trust Terriroty of the Pacific Islands)" ,  "PW", " +680 "},
                {"Paraguay " ,  "PY", " +595 "},
                {"Qatar, State of " ,  "QA", " +974  "},
                {"Reunion (French) (Former Bourbon Island)" ,  "RE", " +262 "},
                {"Romania " ,  "RO", " +40 "},
                {"Serbia, Republic of" ,  "RS", " +221 "},
                {"Russian Federation " ,  "RU", " +7 "},
                {"Rwanda (Rwandese Republic) (Former Ruanda)" ,  "RW", " +250 "},
                {"Saudi Arabia " ,  "SA", " +966 "},
                {"Solomon Islands (Former British Solomon Islands)" ,  "SB", " +677 "},
                {"Seychelles " ,  "SC", " +248 "},
                {"Sudan (Former Anglo-Egyptian Sudan) " ,  "SD", " +249 "},
                {"Sweden " ,  "SE", " +46 "},
                {"Singapore " ,  "SG", " +65 "},
                {"Saint Helena " ,  "SH", " +290 "},
                {"Slovenia " ,  "SI", " +386 "},
                {"Svalbard (Spitzbergen) and Jan Mayen Islands " ,  "SJ", "  "},
                {"Slovakia" ,  "SK", " +421 "},
                {"Sierra Leone " ,  "SL", " +232 "},
                {"San Marino " ,  "SM", " +378 "},
                {"Senegal " ,  "SN", "  "},
                {"Somalia (Former Somali Republic, Somali Democratic Republic) " ,  "SO", " +252 "},
                {"Suriname (Former Netherlands Guiana, Dutch Guiana)" ,  "SR", " +597 "},
                {"Sao Tome and Principe " ,  "ST", " +239 "},
                {"Russia - USSR (Former Russian Empire, Union of Soviet Socialist Republics, Russian Soviet Federative Socialist Republic) Now RU - Russian Federation" ,  "SU", "  "},
                {"El Salvador " ,  "SV", " +503 "},
                {"Syria (Syrian Arab Republic) (Former United Arab Republic - with Egypt)" ,  "SY", " +963 "},
                {"Swaziland, Kingdom of " ,  "SZ", " +268 "},
                {"Turks and Caicos Islands " ,  "TC", " +1-649 "},
                {"Chad " ,  "TD", " +235 "},
                {"Tromelin Island " ,  "TE", "  "},
                {"French Southern Territories and Antarctic Lands " ,  "TF", "  "},
                {"Togo (Former French Togoland)" ,  "TG", "  "},
                {"Thailand (Former Siam)" ,  "TH", " +66 "},
                {"Tajikistan (Former Tajik Soviet Socialist Republic)" ,  "TJ", " +992 "},
                {"Tokelau " ,  "TK", " +690 "},
                {"Turkmenistan (Former Turkmen Soviet Socialist Republic)" ,  "TM", " +993 "},
                {"Tunisia " ,  "TN", " +216 "},
                {"Tonga, Kingdom of (Former Friendly Islands)" ,  "TO", " +676 "},
                {"East Timor (Former Portuguese Timor)" ,  "TP", " +670 "},
                {"Turkey " ,  "TR", " +90 "},
                {"Trinidad and Tobago " ,  "TT", " +1-868 "},
                {"Tuvalu (Former Ellice Islands)" ,  "TV", " +688 "},
                {"Taiwan (Former Formosa)" ,  "TW", " +886 "},
                {"Tanzania, United Republic of (Former United Republic of Tanganyika and Zanzibar)" ,  "TZ", " +255 "},
                {"Ukraine (Former Ukrainian National Republic, Ukrainian State, Ukrainian Soviet Socialist Republic)" ,  "UA", " +380 "},
                {"Uganda, Republic of" ,  "UG", " +256 "},
                {"United States Minor Outlying Islands " ,  "UM", "  "},
                {"United States " ,  "US", " +1 "},
                {"Uruguay, Oriental Republic of (Former Banda Oriental, Cisplatine Province)" ,  "UY", " +598 "},
                {"Uzbekistan (Former UZbek Soviet Socialist Republic)" ,  "UZ", " +998 "},
                {"Holy See (Vatican City State)" ,  "VA", "  "},
                {"Vatican City State (Holy See)" ,  "VA", " +418 "},
                {"Saint Vincent and the Grenadines " ,  "VC", " +1-784 "},
                {"Venezuela " ,  "VE", " +58 "},
                {"Virgin Islands, British " ,  "VI", " +1-284 "},
                {"Vietnam " ,  "VN", " +84 "},
                {"Virgin Islands, United States (Former Danish West Indies) " ,  "VQ", " +1-340 "},
                {"Vanuatu (Former New Hebrides)" ,  "VU", " +678 "},
                {"Wallis and Futuna Islands " ,  "WF", " +681 "},
                {"Samoa (Former Western Samoa)" ,  "WS", " +685 "},
                {"Yemen " ,  "YE", " +967 "},
                {"Mayotte (Territorial Collectivity of Mayotte)" ,  "YT", " +269 "},
                {"Yugoslavia " ,  "YU", "  "},
                {"South Africa (Former Union of South Africa)" ,  "ZA", " +27 "},
                {"Zambia, Republic of (Former Northern Rhodesia) " ,  "ZM", " +260 "},
                {"Zaire (Former Congo Free State, Belgian Congo, Congo/Leopoldville, Congo/Kinshasa, Zaire) Now CD - Congo, Democratic Republic of the " ,  "ZR", "  "},
                {"Zimbabwe, Republic of (Former Southern Rhodesia, Rhodesia) " ,  "ZW", " +263 "}
            };
            return a;
        }
    }
}
