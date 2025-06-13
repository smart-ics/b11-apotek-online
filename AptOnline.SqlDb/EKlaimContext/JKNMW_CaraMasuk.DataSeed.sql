INSERT INTO JKNMW_CaraMasuk (CaraMasukId, CaraMasukName)
SELECT 'gp', 'Rujukan KFTP' UNION
SELECT 'hosp-trans', 'Rujukan FKRTL' UNION
SELECT 'mp', 'Rujukan Spesialis' UNION
SELECT 'outp', 'Dari Rawat Jalan' UNION
SELECT 'inp', 'Dari Rawat Inap' UNION
SELECT 'emd', 'Dari Rawat Darurat' UNION
SELECT 'born', 'Lahir Di RS' UNION
SELECT 'nursing', 'Rujukan Panti Jompo' UNION
SELECT 'psych', 'Rujukan Dari RS Jiwa' UNION
SELECT 'rehab', 'Rujukan Fasilitas Rehab' UNION
SELECT 'other', 'Lain-Lain'
GO