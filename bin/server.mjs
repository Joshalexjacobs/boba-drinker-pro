#!/usr/bin/env node

import http from 'http';
import { readFile } from 'fs/promises';
import { basename, join } from 'path';

http
  .createServer(async (request, response) => {
    var filePath = request.url?.replace(/^\//, '') || 'index.html';

    var fullPath = join('./Builds/WebGL/', filePath);

    console.log(`Serving: ${fullPath}`);

    var extension = basename(fullPath).match(/\..+/);
    var contentType = 'text/html';
    var contentEncoding = '';

    if (extension) {
      switch (extension[0]) {
        case '.js':
          contentType = 'text/javascript';
          break;
        case '.css':
          contentType = 'text/css';
          break;
        case '.json':
          contentType = 'application/json';
          break;
        case '.png':
          contentType = 'image/png';
          break;
        case '.jpg':
          contentType = 'image/jpg';
          break;
        case '.wasm.gz':
          contentType = 'application/wasm';
          contentEncoding = 'gzip';
          break;
        case '.data.gz':
        case '.framework.js.gz':
        case '.loader.gz':
          contentType = 'application/javascript';
          contentEncoding = 'gzip';
          break;
      }
    }

    try {
      const contents = await readFile(fullPath);

      response.writeHead(200, {
        'Content-Type': contentType,
        'Content-Encoding': contentEncoding,
      });

      response.end(contents, 'utf-8');
    } catch (error) {
      console.log(error);
    }
  })
  .listen(8080);

console.log('Server running at http://localhost:8080/');
