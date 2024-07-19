document.addEventListener('DOMContentLoaded', function() {
    loadComponent('sidebar-container', 'components/sidebar.html');
    loadComponent('popups-container', 'components/popups.html');
    initializeMainContentContainer();
  
    function loadComponent(containerId, componentFile) {
      return fetch(componentFile)
        .then(response => response.text())
        .then(data => {
          document.getElementById(containerId).innerHTML = data;
        })
        .catch(error => console.error('Error loading component:', error));
    }
  
    function initializeMainContentContainer() {
      document.getElementById('main-content-container').innerHTML = `
        <div class="main-content">
          <h1>Inventory</h1>
          <div class="controls">
            <select id="sourceDropdown">
              <option value="All Sources">All Sources</option>
              <option value="WSUS">WSUS</option>
              <option value="ThirdParty">ThirdParty</option>
            </select>
            <input type="text" id="searchInput" placeholder="Search...">
            <button id="addFilterBtn">Add filter</button>
            <select class="status-dropdown" id="statusDropdown">
              <option value="">To status</option>
              <option value="Testing">Testing</option>
              <option value="Approved">Approved</option>
              <option value="Unapproved">Unapproved</option>
              <option value="Ignored">Ignored</option>
              <option value="Downloading">Downloading</option>
              <option value="Incomplete">Incomplete</option>
            </select>
          </div>
          
          <div class="table-container">
            <table class="table">
              <thead>
                <tr>
                  <th data-sort="id"><input type="checkbox" id="checkAll"></th>
                  <th data-sort="title">ID</th>
                  <th data-sort="source">Title</th>
                  <th data-sort="software">Source</th>
                  <th data-sort="language">Software</th>
                  <th data-sort="severity">Language</th>
                  <th data-sort="os">Severity</th>
                  <th data-sort="architecture">OS</th>
                  <th data-sort="status">Architecture</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody id="tableBody">
                <!-- 더미 데이터 파일 로드 -->
              </tbody>
            </table>
          </div>
          
          <div class="pagination">
            <button id="prevPage" disabled> &lt; </button>
            <span id="pageInfo">1 to 4 of 4</span>
            <button id="nextPage"> &gt; </button>
          </div>
          
          <div class="details-section" id="detailsSection">
            <div class="details-header">
              <h2 id="detailsTitle">Details</h2>
            </div>
            <div class="details-tabs">
              <button class="active" data-tab="description">Description</button>
              <button data-tab="test">Test</button>
              <button data-tab="logs">Logs</button>
            </div>
            <div class="details-content active" id="description">
              <!-- Description content -->
            </div>
            <div class="details-content" id="test">
              <!-- Test content -->
              <p>Change Status:</p>
              <select id="testStatus">
                <option value="Pending">Pending</option>
                <option value="Testing">Testing</option>
                <option value="Approved">Approved</option>
                <option value="Rejected">Rejected</option>
              </select>
              <p>Notes:</p>
              <textarea id="testNotes" rows="4" cols="50"></textarea>
              <p>Required UI:</p>
              <input type="text" id="requiredUI">
              <p>Required Reboot:</p>
              <input type="text" id="requiredReboot">
              <p>UI Auto Script:</p>
              <input type="text" id="uiAutoScript">
              <p>Applicability Rules (XML):</p>
              <textarea id="applicabilityRules" rows="4" cols="50"></textarea>
            </div>
            <div class="details-content" id="logs">
              <!-- Logs content -->
            </div>
          </div>
        </div>
      `;
      loadInventoryPage();
    }
  
    function loadInventoryPage() {
      loadComponent('main-content-container', 'components/inventory.html')
        .then(() => {
          loadDummyData().then(() => {
            initializeEventListeners();
            updatePagination();
          });
        })
        .catch(error => console.error('Error loading inventory page:', error));
    }
  
    function loadDummyData() {
      return fetch('components/dummy-data.html')
        .then(response => response.text())
        .then(data => {
          document.getElementById('tableBody').innerHTML = data;
        })
        .catch(error => console.error('Error loading dummy data:', error));
    }
  
    document.getElementById('sidebar-container').addEventListener('click', function(event) {
      if (event.target && event.target.textContent === 'Inventory') {
        loadInventoryPage();
        window.scrollTo(0, 0); // 페이지를 최상단으로 스크롤
      }
    });
  
    let currentPage = 1;
    const rowsPerPage = 10;
    let totalRows;
  
    function initializeEventListeners() {
      document.getElementById('addFilterBtn')?.addEventListener('click', function() {
        document.getElementById('overlay').style.display = 'block';
        document.getElementById('filterPopup').style.display = 'block';
      });
  
      document.getElementById('overlay')?.addEventListener('click', function() {
        closePopup('filterPopup');
      });
  
      function closePopup(popupId) {
        document.getElementById('overlay').style.display = 'none';
        document.getElementById(popupId).style.display = 'none';
      }
  
      document.querySelectorAll('.table tbody tr').forEach(row => {
        row.addEventListener('click', function(event) {
          if (event.target.type !== 'checkbox') {
            showDetails(row);
          }
        });
      });
  
      document.querySelectorAll('.details-tabs button').forEach(button => {
        button.addEventListener('click', function() {
          const tab = this.getAttribute('data-tab');
          document.querySelectorAll('.details-tabs button').forEach(btn => btn.classList.remove('active'));
          document.querySelectorAll('.details-content').forEach(content => content.classList.remove('active'));
          this.classList.add('active');
          document.getElementById(tab).classList.add('active');
        });
      });
  
      function showDetails(row) {
        const title = row.getAttribute('data-title');
        const detailsTitleElement = document.getElementById('detailsTitle');
        const descriptionElement = document.getElementById('description');
  
        if (detailsTitleElement && descriptionElement) {
          detailsTitleElement.innerText = title;
          descriptionElement.innerHTML = `
            <p><strong>ID:</strong> ${row.getAttribute('data-id')}</p>
            <p><strong>Title:</strong> ${title}</p>
            <p><strong>Source:</strong> ${row.getAttribute('data-source')}</p>
            <p><strong>Software:</strong> ${row.getAttribute('data-software')}</p>
            <p><strong>Language:</strong> ${row.getAttribute('data-language')}</p>
            <p><strong>Severity:</strong> ${row.getAttribute('data-severity')}</p>
            <p><strong>OS:</strong> ${row.getAttribute('data-os')}</p>
            <p><strong>Architecture:</strong> ${row.getAttribute('data-architecture')}</p>
            <p><strong>Status:</strong> ${row.getAttribute('data-status')}</p>
          `;
        }
      }
  
      document.getElementById('sourceDropdown')?.addEventListener('change', function() {
        const source = this.value;
        const rows = document.querySelectorAll('.table tbody tr');
        const searchInput = document.getElementById('searchInput');
  
        rows.forEach(row => {
          if (source === 'All Sources' || row.getAttribute('data-source') === source) {
            row.style.display = '';
          } else {
            row.style.display = 'none';
          }
        });
  
        searchInput.value = source === 'All Sources' ? '' : source;
      });
  
      document.getElementById('statusDropdown')?.addEventListener('change', function() {
        const status = this.value;
        const checkboxes = document.querySelectorAll('.row-checkbox:checked');
  
        checkboxes.forEach(checkbox => {
          const row = checkbox.closest('tr');
          row.setAttribute('data-status', status);
          row.querySelector('td:last-child').innerText = status;
          checkbox.checked = false;
        });
  
        document.getElementById('checkAll').checked = false;
        this.value = '';
      });
  
      document.getElementById('checkAll')?.addEventListener('change', function() {
        const checkboxes = document.querySelectorAll('.row-checkbox');
        checkboxes.forEach(checkbox => {
          checkbox.checked = this.checked;
        });
      });
  
      document.getElementById('searchInput')?.addEventListener('input', function() {
        const searchValue = this.value.toLowerCase();
        const rows = document.querySelectorAll('.table tbody tr');
  
        rows.forEach(row => {
          const title = row.getAttribute('data-title').toLowerCase();
          const source = row.getAttribute('data-source').toLowerCase();
          const software = row.getAttribute('data-software').toLowerCase();
          const language = row.getAttribute('data-language').toLowerCase();
          const severity = row.getAttribute('data-severity').toLowerCase();
          const os = row.getAttribute('data-os').toLowerCase();
          const architecture = row.getAttribute('data-architecture').toLowerCase();
          const status = row.getAttribute('data-status').toLowerCase();
  
          if (
            title.includes(searchValue) ||
            source.includes(searchValue) ||
            software.includes(searchValue) ||
            language.includes(searchValue) ||
            severity.includes(searchValue) ||
            os.includes(searchValue) ||
            architecture.includes(searchValue) ||
            status.includes(searchValue)
          ) {
            row.style.display = '';
          } else {
            row.style.display = 'none';
          }
        });
      });
  
      document.querySelectorAll('.table th[data-sort]').forEach(header => {
        header.addEventListener('click', function() {
          const sortField = this.getAttribute('data-sort');
          sortTableByField(sortField);
        });
      });
  
      document.getElementById('prevPage')?.addEventListener('click', function() {
        if (currentPage > 1) {
          currentPage--;
          updateTable();
        }
      });
  
      document.getElementById('nextPage')?.addEventListener('click', function() {
        if (currentPage * rowsPerPage < totalRows) {
          currentPage++;
          updateTable();
        }
      });
  
      function sortTableByField(field) {
        const rowsArray = Array.from(document.querySelectorAll('.table tbody tr'));
        const sortedRows = rowsArray.sort((a, b) => {
          const aField = a.getAttribute(`data-${field}`).toLowerCase();
          const bField = b.getAttribute(`data-${field}`).toLowerCase();
          if (aField < bField) return -1;
          if (aField > bField) return 1;
          return 0;
        });
  
        const tableBody = document.getElementById('tableBody');
        tableBody.innerHTML = '';
        sortedRows.forEach(row => tableBody.appendChild(row));
      }
  
      function updatePagination() {
        const rows = document.querySelectorAll('.table tbody tr');
        totalRows = rows.length;
  
        const start = (currentPage - 1) * rowsPerPage;
        const end = start + rowsPerPage;
        rows.forEach((row, index) => {
          row.style.display = index >= start && index < end ? '' : 'none';
        });
  
        document.getElementById('prevPage').disabled = currentPage === 1;
        document.getElementById('nextPage').disabled = end >= totalRows;
  
        document.getElementById('pageInfo').innerText = `${start + 1} to ${Math.min(end, totalRows)} of ${totalRows}`;
      }
  
      updatePagination();
    }
  });
  