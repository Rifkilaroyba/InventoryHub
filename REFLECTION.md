# Refleksi: Membangun InventoryHub dengan Microsoft Copilot

> Draf awal - ubah dengan pengalaman dan kata-kata Anda sendiri sebelum dikirim.

## Bagaimana Copilot membantu
- **Kode integrasi:** Copilot membantu membuat `HttpClient`, `ProductService`, dan endpoint `/api/productlist`, serta mengonfigurasi CORS antara Blazor (port 5001) dan API (port 5000).
- **Debugging:** Copilot membantu menemukan penyebab umum masalah integrasi: CORS, URL dasar yang salah, dan ketidakcocokan nama properti JSON.
- **Struktur JSON:** Copilot menyarankan objek `category` bersarang, nama properti camelCase, dan harga bertipe `decimal`. Contoh di instruksi memakai `1200,50` (koma, format Indonesia) dan `baru` (terjemahan dari `new`); keduanya harus diganti menjadi `1200.50m` dan `new` agar kode bisa dikompilasi.
- **Optimasi kinerja:** Copilot menyarankan `IMemoryCache` dan header `Cache-Control` di back-end, serta cache sisi klien dan penggabungan permintaan bersamaan di front-end.

## Tantangan dan solusinya
1. **Kesalahan CORS:** browser memblokir permintaan lintas port. Solusi: kebijakan CORS yang membatasi asal ke `http://localhost:5001`.
2. **Data tidak tampil karena beda nama properti:** diselesaikan dengan menyamakan model C# dengan JSON back-end.
3. **Panggilan API berulang:** diatasi dengan cache di `ProductService` dan pemuatan data hanya sekali di `OnInitializedAsync`.

## Pelajaran tentang menggunakan Copilot secara efektif
- Prompt yang spesifik (menyebut framework, struktur data, dan batasan) menghasilkan saran yang jauh lebih tepat.
- Saran Copilot harus selalu diuji dan ditinjau; tidak semua saran langsung benar.
- Copilot paling berguna untuk kode berulang, ide optimasi, dan menjelaskan pesan galat, sementara keputusan arsitektur tetap di tangan pengembang.
- Ukur dulu sebelum dan sesudah optimasi (tab Network di DevTools) agar peningkatan dapat dibuktikan.
