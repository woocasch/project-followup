/** @type {import('@eventcatalog/core/bin/eventcatalog.config').Config} */
export default {
  title: 'Project Follow-Up Event Catalog',
  tagline: 'Architectural catalog for Project Follow-Up application',
  organizationName: 'LukaszNowakowski.it',
  homepageLink: 'https://eventcatalog.dev/',
  editUrl: 'https://github.com/woocasch/project-followup',
  // Supports static or server. Static renders a static site, server renders a server side rendered site
  // large catalogs may benefit from server side rendering
  output: 'static',
  // By default set to false, add true to get urls ending in /
  trailingSlash: false,
  // Change to make the base url of the site different, by default https://{website}.com/docs,
  // changing to /company would be https://{website}.com/company/docs,
  base: '/',
  // Customize the logo, add your logo to public/ folder
  logo: {
    alt: 'EventCatalog Logo',
    src: '/logo.png',
    text: 'EventCatalog'
  },
  docs: {
    sidebar: {
      // TREE_VIEW will render the DOCS as a tree view and map your file system folder structure
      // LIST_VIEW will render the DOCS that look familiar to API documentation websites
      type: 'LIST_VIEW'
    },
  },
  // Enable RSS feed for your eventcatalog
  rss: {
    enabled: true,
    // number of items to include in the feed per resource (event, service, etc)
    limit: 20
  },
  // This lets you copy markdown contents from EventCatalog to your clipboard
  // Including schemas for your events and services
  llmsTxt: {
    enabled: true,
  },
  // required random generated id used by eventcatalog
  cId: 'c95eec32-f99d-4ac2-903e-592daf841804',
  visualiser: {
    channels: {
      renderMode: 'flat'
    }
  }
}
